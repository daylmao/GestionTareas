using AutoMapper;
using GestionTareas.Context;
using GestionTareas.DTOs;
using GestionTareas.Enum;
using GestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GestionTareas.Service
{
    public class TareaService : ITareaService
    {
        private readonly GestorTareasContext _context;
        private readonly IMapper _mapper;

        public TareaService(GestorTareasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TareaDTO> CreateAsync(CreateTareaDTO CreateTarea)
        {
            var tarea = _mapper.Map<Tarea>(CreateTarea);
            if (tarea == null)
            {
                return null;
            }
            await _context.Set<Tarea>().AddAsync(tarea);
            await _context.SaveChangesAsync();
            return _mapper.Map<TareaDTO>(tarea);
        }

        public async Task<TareaDTO> DeleteAsync(int id)
        {
            var found = await _context.Set<Tarea>().FindAsync(id);
            if (found == null)
            {
                return null;
            }
            _context.Set<Tarea>().Remove(found);
            await _context.SaveChangesAsync();
            return _mapper.Map<TareaDTO>(found);
        }

        public async Task<IEnumerable<TareaDTO>> FilterByStatus(Status status)
        {
            var query = _context.Set<Tarea>().Where(b => b.Status == status);
            var lista = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TareaDTO>>(lista);
        }

        public async Task<IEnumerable<TareaDTO>> GetAllAsync()
        {
           IEnumerable<Tarea> tarea =await _context.Set<Tarea>().ToListAsync();  
            return _mapper.Map<IEnumerable<TareaDTO>>(tarea);
        }

        public async Task<TareaDTO> UpdateAsync(int id, UpdateTareaDTO update)
        {
            var found = await _context.Set<Tarea>().FindAsync(id);
            if (found == null)
            {
                return null;
            }
            var Update = _mapper.Map<Tarea>(update);
            _context.Set<Tarea>().Attach(found);
            _context.Set<Tarea>().Entry(found).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<TareaDTO>(Update);
            
        }

       
    }
}
