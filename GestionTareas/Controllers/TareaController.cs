using Microsoft.AspNetCore.Mvc;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Domain.Enum;

namespace GestionTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTareaDTO create)
        {
            var tarea = await _tareaService.CreateAsync(create);
            return tarea == null? NotFound() : Ok(tarea);
        }

        [HttpGet]
        public async Task<IEnumerable<TareaDTO>> GetAllAsync() => await _tareaService.GetAllAsync();

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id , [FromBody] UpdateTareaDTO update)
        {
            if (update == null)
            {
                return BadRequest();
            }
            var UpdatedUser = await _tareaService.UpdateAsync(id,update);
            if (UpdatedUser == null)
            {
                return NotFound();
            }
            return Ok(UpdatedUser);
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<IEnumerable<TareaDTO>>> FilterByStatusAsync([FromRoute] Status status)
        {
            var filtered = await _tareaService.FilterByStatus(status);
            return filtered == null? BadRequest() : Ok(filtered);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TareaDTO>> DeleteAsync([FromRoute] int id)
        {
            var deleted = await _tareaService.DeleteAsync(id);
            return deleted == null ? BadRequest(): Ok(deleted);

        }
    }
}
