using GestionTareas.Core.Application.DTOs.Account.Authenticate;
using GestionTareas.Core.Application.DTOs.Account.Register;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Domain.Enum;
using GestionTareas.Core.Domain.Settings;
using GestionTareas.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Infraestructure.Identity.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _jwtSettings;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
        {
            AuthenticateResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con: {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Credenciales invalidas";
                return response;

            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = "Email no confirmado";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateTokenAsync(user);

            response.Id = user.Id;
            response.Username = user.UserName;
            response.FirstName = user.firstName;
            response.LastName = user.lastName;
            response.Email = user.Email;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<RegisterResponse> RegisterAccountAsync(RegisterRequest request, string roles)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };
            var userSameUsername = await _userManager.FindByNameAsync(request.Username);
            
            if (userSameUsername != null)
            {
                response.HasError = true;
                response.Error = "Username already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);


            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = "Email already registered";
                return response;
            }

            User user = new()
            {
                
                
                UserName = request.Username,
                firstName = request.FirstName,
                lastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                EmailConfirmed = true
                
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roles);
            }

            else
            {
                response.HasError = true;
                response.Error = "error occurred trying to register the user";
                
            }

            return response;

        }

        private async Task<JwtSecurityToken> GenerateTokenAsync(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> rolesclaims = new List<Claim>();
            foreach (var role in roles)
            {
                rolesclaims.Add(new Claim("roles", role));
            }
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id)
            }
            .Union(userClaims)
            .Union(rolesclaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claim,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials

                );
            return jwtSecurityToken;

        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),

                Expires = DateTime.UtcNow.AddDays(7),

                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var randomBytes = new Byte[40];
            randomNumberGenerator.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
