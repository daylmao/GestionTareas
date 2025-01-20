using GestionTareas.Enum;

namespace GestionTareas.DTOs
{
    public class TareaDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateOnly DueDate { get; set; }
        public Status? Status { get; set; }
        public int AdditionalData { get; set; }
    }
}
