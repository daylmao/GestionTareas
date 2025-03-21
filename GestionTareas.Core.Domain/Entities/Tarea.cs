﻿using GestionTareas.Core.Domain.Enum;

namespace GestionTareas.Core.Domain.Entities
{
    public class Tarea
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status? Status { get; set; }
        public int AdditionalData { get; set; }
    }
}
