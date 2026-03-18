using SmartCity.Application.DTOs;
using SmartCity.Application.Interfaces;
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

        public async Task<IEnumerable<Incident>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Incident?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Incident> CreateAsync(CreateIncidentDto dto)
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

            return incident;
        }

        public async Task DeleteAsync(Guid id)
        {
            var incident = await _repository.GetByIdAsync(id);

            if (incident is null)
                throw new Exception("Incident not found");

            _repository.Delete(incident);
            await _repository.SaveChangesAsync();
        }
    }
}
