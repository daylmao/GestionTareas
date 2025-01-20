using GestionTareas.DTOs;
using GestionTareas.Enum;
using GestionTareas.Core.Domain.Entities;

namespace GestionTareas.Core.Application.Interfaces.Service
{
    public interface ITareaService: IGenericTareaService<TareaDTO,CreateTareaDTO,UpdateTareaDTO>
    {

    }
}
