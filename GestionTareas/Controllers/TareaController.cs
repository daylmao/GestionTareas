﻿using Microsoft.AspNetCore.Mvc;
using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Domain.Enum;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost("create-tarea")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> CreateTarea([FromBody] CreateTareaDTO create)
        {
            var result = await _tareaService.CreateAsync(create);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpPost("high-priority")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> CreateHighPriorityTarea([FromBody] string description)
        {
            var result = await _tareaService.CreateHighPriority(description);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpPost("low-priority")]
        
        public async Task<IActionResult> CreateLowPriorityTarea([FromBody] string description)
        {
            var result = await _tareaService.CreateLowPriority(description);

            if (result.IsSuccess)
                return Ok(result.Data);
         
            return BadRequest(result.Error);
        }

        [HttpGet]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _tareaService.GetAllAsync();

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tareaService.GetByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.Error);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateTareaDTO update)
        {
            var result = await _tareaService.UpdateAsync(id, update);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "Basic")]
        public async Task<ActionResult<IEnumerable<TareaDTO>>> FilterByStatusAsync([FromRoute] Status status)
        {
            var result = await _tareaService.FilterByStatus(status);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<ActionResult<TareaDTO>> DeleteAsync([FromRoute] int id)
        {
            var result = await _tareaService.DeleteAsync(id);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.Error);
        }

    }
}
