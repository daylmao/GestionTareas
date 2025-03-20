using Microsoft.AspNetCore.Identity;

namespace GestionTareas.Infraestructure.Identity.Entities
{
    public class User: IdentityUser
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }
}
