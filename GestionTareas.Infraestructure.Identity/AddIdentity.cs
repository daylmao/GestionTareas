using GestionTareas.Core.Application.DTOs.Account.JWT;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Domain.Settings;
using GestionTareas.Infraestructure.Identity.Context;
using GestionTareas.Infraestructure.Identity.Entities;
using GestionTareas.Infraestructure.Identity.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Infraestructure.Identity
{
    public static class AddIdentity
    {
        #region Connection
        public static void AddIdentityMethod(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(b =>
            {
                b.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                c => c.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

        #endregion

        #region Identity
        services.AddIdentity<User,IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();


            #endregion

            #region JWT
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },

                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JWTResponse { HasError = true, Error = "You aren't Authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JWTResponse { HasError = true, Error = "You aren't Authorized to access to this content" });
                        return c.Response.WriteAsync(result);
                    }
                };

            });

            services.AddScoped<IAccountService, AccountService>();
            #endregion

        }
    }
}
