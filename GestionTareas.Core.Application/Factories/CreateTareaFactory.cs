using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Domain.Entities;
using GestionTareas.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.Factories
{
    public class CreateTareaFactory : TareaFactory
    {
        public override Tarea CreateHighPriorityTarea(string description)
        {
            return new Tarea
            {
                Description = description,
                DueDate = DateTime.UtcNow.AddDays(1),
                Status = Status.Pendiente,
                AdditionalData = 1
            };
        }

        public override Tarea CreateLowPriorityTarea(string description)
        {
            return new Tarea
            {
                Description = description,
                DueDate = DateTime.UtcNow.AddDays(5),
                Status = Status.Pendiente,
                AdditionalData = 3
            };
        }
    }
}
