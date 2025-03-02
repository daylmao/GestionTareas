using GestionTareas.Core.Application.DTOs.Account.Authenticate;
using GestionTareas.Core.Application.DTOs.Account.Register;
using GestionTareas.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.Interfaces.Service
{
    public interface IAccountService
    {   
        Task<RegisterResponse> RegisterAccountAsync(RegisterRequest request, string roles);
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);
        

    }
}
