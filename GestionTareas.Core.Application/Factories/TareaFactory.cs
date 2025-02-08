using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Domain.Entities;


namespace GestionTareas.Core.Application.Factories
{
    public abstract class TareaFactory
    {
        public abstract Tarea CreateHighPriorityTarea(string description);
        public abstract Tarea CreateLowPriorityTarea(string description);
    }
}
