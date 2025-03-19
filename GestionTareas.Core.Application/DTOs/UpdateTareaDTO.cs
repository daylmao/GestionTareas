using GestionTareas.Core.Domain.Enum;

namespace GestionTareas.Core.Application.DTOs
{
    public class UpdateTareaDTO
    {
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public int AdditionalData { get; set; }
    }
}
