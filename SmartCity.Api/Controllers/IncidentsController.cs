using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartCity.Application.DTOs;
using SmartCity.Application.Interfaces;

namespace SmartCity.Api.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _service;
        private readonly IValidator<CreateIncidentDto> _validator;  

        public IncidentsController(
            IIncidentService service,
            IValidator<CreateIncidentDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var incident = await _service.GetByIdAsync(id);

            if (incident is null)
                return NotFound();

            return Ok(incident);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateIncidentDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var incident = await _service.CreateAsync(dto);
            return Ok(incident);    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
