using GestionTareas.Enum;

namespace GestionTareas.DTOs
{
    public class UpdateTareaDTO
    {
        public string? Description { get; set; }
        public Status? Status { get; set; }
    }
}
