using GestionTareas.Core.Domain.Entities;


namespace GestionTareas.Core.Application.Interfaces.Repository
{
    public interface ITareaRepository : IGenericRepository<Tarea>
    {

        bool Validate(Func<Tarea, bool> validate);


    }
}
