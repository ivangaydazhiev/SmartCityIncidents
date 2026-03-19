using SmartCity.Application.DTOs;
using SmartCity.Domain.Entities;

namespace SmartCity.Application.Mappings
{
    public static class IncidentMappings
    {
        public static IncidentResponseDto ToDto(this  Incident incident)
        {
            return new IncidentResponseDto
            {
                Id = incident.Id,
                Title = incident.Title,
                Description = incident.Description,
                Type = (int)incident.Type,
                Status = (int)incident.Status,
                CreatedAt = incident.CreatedAt,
                Location = incident.Location == null ? null : new LocationDto
                {
                    City = incident.Location.City,
                    Address = incident.Location.Address
                }
            };
        }
    }
}
