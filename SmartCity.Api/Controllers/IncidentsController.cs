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

        public IncidentsController(IIncidentService service)
        {
            _service = service;
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
