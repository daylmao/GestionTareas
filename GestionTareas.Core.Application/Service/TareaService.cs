using AutoMapper;
using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Application.Factories;
using GestionTareas.Core.Application.Hub;
using GestionTareas.Core.Application.Interfaces.Repository;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Domain.Entities;
using GestionTareas.Core.Domain.Enum;
using Microsoft.AspNetCore.SignalR;

namespace GestionTareas.Core.Application.Service
{
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;
        private readonly TareaValidadorService _tarea;
        private readonly TareaFactory _tareaFactory;
        private Queue<Tarea> _cola = new Queue<Tarea>();
        private Dictionary<Status, IEnumerable<Tarea>> _keyValues = new();
        private readonly IHubContext<NotificationHub> _hubContext;

        public TareaService(ITareaRepository tareaRepository, IMapper mapper, TareaValidadorService tarea, TareaFactory tareaFactory,  IHubContext<NotificationHub> hubContext)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _tarea = tarea;
            _tareaFactory = tareaFactory;
            _hubContext = hubContext;
        }

        public async Task<Result<TareaDTO>> CreateHighPriority(string description)
        {
            var found = _tareaRepository.Validate(t => t.Description == description);

            if (found)
                return Result<TareaDTO>.Failure("409", "Ya existe una tarea con esa descripcion");

            var tarea = _tareaFactory.CreateHighPriorityTarea(description);

            _cola.Enqueue(tarea);
            while (_cola.Count > 0)
            {
                await _tareaRepository.CreateAsync(_cola.Dequeue());
            }
            _tarea.Notificar(tarea);
            
            var tareaDTO = _mapper.Map<TareaDTO>(tarea);
            await _hubContext.Clients.All.SendAsync("SendNotification", "Tarea de alta prioridad creada exitosamente");

            return Result<TareaDTO>.Success(tareaDTO, "200");
        }

        public async Task<Result<TareaDTO>> CreateLowPriority(string description)
        {
            var found = _tareaRepository.Validate(t => t.Description == description);

            if (found)
                return Result<TareaDTO>.Failure("409", "Ya existe una tarea con esa descripcion");

            var tarea = _tareaFactory.CreateLowPriorityTarea(description);

            _cola.Enqueue(tarea);
            while (_cola.Count > 0)
            {
                await _tareaRepository.CreateAsync(_cola.Dequeue());
            }
            _tarea.Notificar(tarea);

            var tareaDTO = _mapper.Map<TareaDTO>(tarea);
            await _hubContext.Clients.All.SendAsync("SendNotification", "Tarea de baja prioridad creada exitosamente");

            return Result<TareaDTO>.Success(tareaDTO, "200");
        }

        public async Task<Result<IEnumerable<TareaDTO>>> GetAllAsync()
        {
            var getAll = await _tareaRepository.GetAllAsync();
            var mappedResult = getAll.Select(b => _mapper.Map<TareaDTO>(b));
            
            return Result<IEnumerable<TareaDTO>>.Success(mappedResult, "200");
        }

        public async Task<Result<TareaDTO>> GetByIdAsync(int id)
        {
            var getById = await _tareaRepository.GetByIdAsync(id);
            if (getById == null)
                return Result<TareaDTO>.Failure("404", "Tarea no encontrada");

            _tarea.DiasRestantes(getById);
            return Result<TareaDTO>.Success(_mapper.Map<TareaDTO>(getById), "200");

        }
        public async Task<Result<TareaDTO>> CreateAsync(CreateTareaDTO create)
        {
            var newInfo = _mapper.Map<Tarea>(create);
            if (newInfo == null)
                return Result<TareaDTO>.Failure("400", "Datos invalidos");

            var found = _tareaRepository.Validate(t => t.Description == newInfo.Description);

            if (found)
                return Result<TareaDTO>.Failure("409", "Ya existe una tarea con esa descripcion");

            var isValid = _tarea.Validar(newInfo);
            if (!isValid)
                return Result<TareaDTO>.Failure("422", "La validacion de la tarea ha fallado");

            _cola.Enqueue(newInfo);
            while (_cola.Count > 0)
            {
                await _tareaRepository.CreateAsync(_cola.Dequeue());
            }
            
            _tarea.Notificar(newInfo);
            await _hubContext.Clients.All.SendAsync("SendNotification", "Tarea creada exitosamente");

            return Result<TareaDTO>.Success(_mapper.Map<TareaDTO>(newInfo), "200");
        }

        public async Task<Result<IEnumerable<TareaDTO>>> FilterByStatus(Status status)
        {
            if (_keyValues.TryGetValue(status, out var cachedTareas))
            {
                var cachedMapped = cachedTareas.Select(b => _mapper.Map<TareaDTO>(b));
                return Result<IEnumerable<TareaDTO>>.Success(cachedMapped, "200");
            }

            var filtered = await _tareaRepository.FilterByStatus(status);
            _keyValues[status] = filtered; 

            var mappedResult = filtered.Select(b => _mapper.Map<TareaDTO>(b));
            return Result<IEnumerable<TareaDTO>>.Success(mappedResult, "200");
        }



        public async Task<Result<TareaDTO>> UpdateAsync(int id,UpdateTareaDTO update)
        {
            var oldData = await _tareaRepository.GetByIdAsync(id);
            if (oldData == null)
                return Result<TareaDTO>.Failure("404", "Tarea no encontrada");

            var isDescriptionExists = _tareaRepository.Validate(t => t.Description == update.Description && t.Id != id);
            if (isDescriptionExists)
                return Result<TareaDTO>.Failure("409", "Ya existe una tarea con esa descripcion");

            var isValid = _tarea.Validar(oldData);
            if (!isValid)
                return Result<TareaDTO>.Failure("422", "La validacion de la tarea ha fallado");

            var newInfo = _mapper.Map(update, oldData);

            _cola.Enqueue(newInfo);
            while (_cola.Count > 0)
            {
            await _tareaRepository.UpdateAsync(_cola.Dequeue());

            }
            return Result<TareaDTO>.Success(_mapper.Map<TareaDTO>(newInfo),"200");
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            var found = await _tareaRepository.GetByIdAsync(id);
            if (found == null)
                return Result<string>.Failure("404", "Tarea no encontrada");

            _cola.Enqueue(found);
            while (_cola.Count > 0)
            {

            await _tareaRepository.DeleteAsync(_cola.Dequeue());

            }
            return Result<string>.Success("Tarea eliminada correctamente", "200");

        }

    }
}
