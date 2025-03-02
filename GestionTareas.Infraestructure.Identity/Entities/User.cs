using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Infraestructure.Identity.Entities
{
    public class User: IdentityUser
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;

    }
}
