using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.DTOs.Account.Register
{
    public class RegisterResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
