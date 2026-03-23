using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCity.Application.DTOs;
using SmartCity.Application.Interfaces;

namespace SmartCity.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("/api[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _service;
        private readonly IValidator<CreateIncidentDto> _validator;
        private readonly IValidator<UpdateIncidentDto> _updateValidator;

        public IncidentsController(
            IIncidentService service,
            IValidator<CreateIncidentDto> validator,
            IValidator<UpdateIncidentDto> updateValidator)
        {
            _service = service;
            _validator = validator;
            _updateValidator = updateValidator;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateIncidentDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var updated = await _service.UpdateAsync(id, dto);

            return Ok(updated);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] IncidentFilterDto filter)
        {
            filter.Page = Math.Max(filter.Page, 1);
            filter.PageSize = Math.Max(filter.PageSize, 1);
            filter.PageSize = Math.Min(filter.PageSize, 50);

            var result = await _service.GetFilteredAsync(filter);   

            return Ok(result);
        }
    }
}
