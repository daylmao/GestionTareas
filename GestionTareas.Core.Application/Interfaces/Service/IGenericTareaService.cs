using GestionTareas.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.Interfaces.Service
{
    public interface IGenericTareaService<T,TI,TU> where T : class where TU : class where TI : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task <T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FilterByStatus(Status status);
        Task<T> CreateAsync(TI create);
        Task<T> UpdateAsync(int id, TU update);
        Task<T> DeleteAsync(int id);
    }
}
