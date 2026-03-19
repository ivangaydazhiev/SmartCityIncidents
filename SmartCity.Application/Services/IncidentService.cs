using SmartCity.Application.DTOs;
using SmartCity.Application.Interfaces;
using SmartCity.Application.Mappings;
using SmartCity.Domain.Entities;
using SmartCity.Domain.Enums;

namespace SmartCity.Application.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repository;

        public IncidentService(IIncidentRepository repository)
        {
           _repository = repository;
        }

        public async Task<IEnumerable<IncidentResponseDto>> GetAllAsync()
        {
            var incidents = await _repository.GetAllAsync();
            return incidents.Select(i => i.ToDto()).ToList();
        }

        public async Task<IncidentResponseDto?> GetByIdAsync(Guid id)
        {
            var incident = await _repository.GetByIdAsync(id);
            return incident?.ToDto();
        }
        public async Task<IncidentResponseDto> CreateAsync(CreateIncidentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title is required");

            var incident = new Incident
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Type = dto.Type,
                Status = IncidentStatus.Reported,
                CreatedAt = DateTime.UtcNow,
                LocationId = dto.LocationId
            };

            await _repository.AddAsync(incident);
            await _repository.SaveChangesAsync();

            var created = await _repository.GetByIdAsync(incident.Id);

            if (created is null)
                throw new Exception("Failed to load created incident");
            
            return created.ToDto();

            
        }

        public async Task DeleteAsync(Guid id)
        {
            var incident = await _repository.GetByIdAsync(id);

            if (incident is null)
                throw new KeyNotFoundException("Incident not found");

            _repository.Delete(incident);
            await _repository.SaveChangesAsync();
        }
    }
}
