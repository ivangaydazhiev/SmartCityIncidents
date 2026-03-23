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

        public async Task<IncidentResponseDto> UpdateAsync(Guid id, UpdateIncidentDto dto)
        {
            var incident = await _repository.GetByIdAsync(id);

            if (incident is null)
                throw new KeyNotFoundException("Incident not found");

            incident.Title = dto.Title;
            incident.Description = dto.Description;
            incident.Type =  dto.Type;
            incident.Status = dto.Status;
            incident.LocationId = dto.LocationId;

            _repository.Update(incident);
            await _repository.SaveChangesAsync();

            var update = await _repository.GetByIdAsync(id);

            return update!.ToDto();
        }

        public async Task<PagedResult<IncidentResponseDto>> GetFilteredAsync(IncidentFilterDto filter)
        {
            var (items, totalCount) = await _repository.GetFilteredAsync(filter);

            return new PagedResult<IncidentResponseDto>
            {
                Items = items.Select(i => i.ToDto()),
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
