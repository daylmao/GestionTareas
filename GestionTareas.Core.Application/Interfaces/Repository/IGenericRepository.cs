using GestionTareas.DTOs;
using GestionTareas.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.Interfaces.Repository
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task <IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync (int id);
        Task CreateAsync(Entity create);
        Task<IEnumerable<Entity>> FilterByStatus(Status status);
        Task UpdateAsync(Entity update);
        Task DeleteAsync(Entity delete);
        Task SavechangesAsync();
    }
}
