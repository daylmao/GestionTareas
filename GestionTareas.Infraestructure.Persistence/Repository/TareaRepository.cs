using GestionTareas.Context;
using GestionTareas.Core.Application.Interfaces.Repository;
using GestionTareas.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GestionTareas.Core.Domain.Enum;

namespace GestionTareas.Infraestructure.Persistence.Repository
{
    public class TareaRepository : ITareaRepository
    {
        private readonly GestorTareasContext _context;

        public TareaRepository(GestorTareasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync() => await _context.Tareas.AsNoTracking().ToListAsync();

        public async Task<Tarea> GetByIdAsync(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task CreateAsync(Tarea create)
        {
            await _context.Tareas.AddAsync(create);
            await SavechangesAsync();
        }
        public async Task<IEnumerable<Tarea>> FilterByStatus(Status status)
        {
            var query = _context.Set<Tarea>().Where(b => b.Status == status);
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Tarea update)
        {
            _context.Tareas.Attach(update);
            _context.Tareas.Entry(update).State = EntityState.Modified;
            await SavechangesAsync();
        }

        public async Task DeleteAsync(Tarea delete)
        {
            _context.Tareas.Remove(delete);
            await SavechangesAsync();
        }

        public async Task SavechangesAsync() => await _context.SaveChangesAsync();

        public bool Validate(Func<Tarea, bool> validate)
        {
            var found = _context.Tareas.AsEnumerable().Any(validate);

            return found;
        }
    }
}
