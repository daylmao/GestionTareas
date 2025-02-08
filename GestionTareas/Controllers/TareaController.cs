using Microsoft.AspNetCore.Mvc;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Domain.Enum;
using GestionTareas.Core.Domain.Entities;

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
        public async Task<IActionResult> Create([FromBody] CreateTareaDTO create)
        {
            var result = await _tareaService.CreateAsync(create);

            if (!result.IsSuccess)

                return StatusCode(result.StatusCode, result.Error);

            return Ok(result.Data); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _tareaService.GetAllAsync();

            if (result.IsSuccess)

                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tareaService.GetByIdAsync(id);
            if (result.IsSuccess)

                return Ok(result.Data);

            return NotFound(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateTareaDTO update)
        {
            var result = await _tareaService.UpdateAsync(id, update);
            if (result.IsSuccess)

                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<TareaDTO>>> FilterByStatusAsync([FromRoute] Status status)
        {
            var result = await _tareaService.FilterByStatus(status);

            if (result.IsSuccess)

                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TareaDTO>> DeleteAsync([FromRoute] int id)
        {
            var result = await _tareaService.DeleteAsync(id);
            if (result.IsSuccess)
            
                return Ok(result.Data);

            return BadRequest(result.Error);
        }
    }
}
