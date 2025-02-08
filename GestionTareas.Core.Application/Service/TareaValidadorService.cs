using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GestionTareas.Core.Application.Service
{
    public class TareaValidadorService
    {
        delegate bool ValidarTarea(Tarea tarea);
        private readonly ILogger<TareaValidadorService> _logger;

        public TareaValidadorService(ILogger<TareaValidadorService> logger)
        {
            _logger = logger;
        }

        public bool Validar(Tarea tarea)
        {
            if (tarea == null)
                return false;

            ValidarTarea validar = t => !string.IsNullOrWhiteSpace(t.Description) && t.DueDate > DateTime.Now;

            return validar(tarea);
        }
        public void Notificar(Tarea tarea)
        {
            Action<Tarea> notificar = t => _logger.LogInformation($"La tarea: {t.Description} ha sido agregada, se vence: {t.DueDate}");

            notificar(tarea);
            DiasRestantes(tarea);
        }

        public void DiasRestantes(Tarea tarea)
        {
            Func<Tarea, int> restantes = t =>
            {
                var diasRestantes = (t.DueDate - DateTime.Now).Days;
                _logger.LogInformation($"Dias restantes: {diasRestantes}");
                return diasRestantes;
            };
            restantes(tarea);
        }

    }
}
