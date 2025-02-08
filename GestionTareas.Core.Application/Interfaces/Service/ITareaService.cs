using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Application.Service;


namespace GestionTareas.Core.Application.Interfaces.Service
{
    public interface ITareaService: IGenericTareaService<TareaDTO,CreateTareaDTO,UpdateTareaDTO>
    {
        Task<Result<TareaDTO>> CreateHighPriority(string description);
        Task<Result<TareaDTO>> CreateLowPriority(string description);
    }
}
