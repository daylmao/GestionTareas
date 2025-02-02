using GestionTareas.Core.Application.Service;
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
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T>> GetByIdAsync(int id);
        Task<Result<IEnumerable<T>>> FilterByStatus(Status status);
        Task<Result<T>> CreateAsync(TI create);
        Task<Result<T>> UpdateAsync(int id, TU update);
        Task<Result<string>> DeleteAsync(int id);
    }
}
