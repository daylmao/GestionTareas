using GestionTareas.DTOs;
using GestionTareas.Enum;
using GestionTareas.Models;

namespace GestionTareas.Service
{
    public interface ITareaService
    {
        Task<TareaDTO> CreateAsync(CreateTareaDTO CreateTarea);
        Task<IEnumerable<TareaDTO>> GetAllAsync();
        Task<IEnumerable<TareaDTO>> FilterByStatus(Status status);
        Task<TareaDTO> DeleteAsync(int id);
        Task<TareaDTO> UpdateAsync(int id, UpdateTareaDTO update);
    }
}
