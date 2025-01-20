using AutoMapper;
using GestionTareas.Core.Application.Interfaces.Repository;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Domain.Entities;
using GestionTareas.DTOs;
using GestionTareas.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.Service
{
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;

        public TareaService(ITareaRepository tareaRepository, IMapper mapper)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TareaDTO>> GetAllAsync()
        {
            var getAll = await _tareaRepository.GetAllAsync();
            return getAll.Select(b => _mapper.Map<TareaDTO>(b));
        }

        public async Task<TareaDTO> GetByIdAsync(int id)
        {
            var getById = await _tareaRepository.GetByIdAsync(id);
            if (getById == null)
            {
                return null;
            }
            return _mapper.Map<TareaDTO>(getById);

        }
        public async Task<TareaDTO> CreateAsync(CreateTareaDTO create)
        {
            var newInfo = _mapper.Map<Tarea>(create);
            if (newInfo == null)
            {
                return null;
            }
            
            await _tareaRepository.CreateAsync(newInfo);
            return _mapper.Map<TareaDTO>(newInfo);
        }

        public async Task<IEnumerable<TareaDTO>> FilterByStatus(Status status)
        {
            var filtered = await _tareaRepository.FilterByStatus(status);
            return filtered.Select(b => _mapper.Map<TareaDTO>(b));
        }

        public async Task<TareaDTO> UpdateAsync(int id,UpdateTareaDTO update)
        {
            var oldData = await _tareaRepository.GetByIdAsync(id);
            if (oldData == null)
            {
                return null;
            }

            var newInfo = _mapper.Map(update, oldData);
            await _tareaRepository.UpdateAsync(newInfo);
            return _mapper.Map<TareaDTO>(newInfo);
        }

        public async Task<TareaDTO> DeleteAsync(int id)
        {
            var found = await _tareaRepository.GetByIdAsync(id);
            if (found == null)
            {
                return null;
            }
            return _mapper.Map<TareaDTO>(found);

        }
    }
}
