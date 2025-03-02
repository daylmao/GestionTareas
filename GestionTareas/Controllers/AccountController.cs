using Azure.Core;
using GestionTareas.Core.Application.DTOs.Account.Authenticate;
using GestionTareas.Core.Application.DTOs.Account.Register;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GestionTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest(new { error = "El cuerpo de la solicitud no puede estar vacío" });
            }

            var result = await _accountService.RegisterAccountAsync(registerRequest, Roles.Basic.ToString().ToLower());

            if (result.HasError)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(result);

        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));

        }
    }
}
