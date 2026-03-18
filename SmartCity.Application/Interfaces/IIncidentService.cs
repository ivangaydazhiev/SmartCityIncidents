using SmartCity.Application.DTOs;
using SmartCity.Domain.Entities;

namespace SmartCity.Application.Interfaces
{
    public interface IIncidentService
    {
        Task<IEnumerable<Incident>> GetAllAsync();
        Task<Incident?> GetByIdAsync (Guid id);
        Task<Incident> CreateAsync(CreateIncidentDto dto);
        Task DeleteAsync(Guid id);
    }
}
