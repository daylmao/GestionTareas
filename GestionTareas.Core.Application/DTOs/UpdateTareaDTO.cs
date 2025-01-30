using GestionTareas.Core.Domain.Enum;

namespace GestionTareas.Core.Application.DTOs
{
    public class UpdateTareaDTO
    {
        public string? Description { get; set; }
        public DateOnly DueDate { get; set; }
        public string? Status { get; set; }
        public int AdditionalData { get; set; }
    }
}
