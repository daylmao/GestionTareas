using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.DTOs.Account.JWT
{
    public class JWTResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
