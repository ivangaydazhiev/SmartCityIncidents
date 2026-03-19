using SmartCity.Application.DTOs;

namespace SmartCity.Application.Interfaces
{
    public interface IIncidentService
    {
        Task<IEnumerable<IncidentResponseDto>> GetAllAsync();
        Task<IncidentResponseDto?> GetByIdAsync (Guid id);
        Task<IncidentResponseDto> CreateAsync(CreateIncidentDto dto);
        Task DeleteAsync(Guid id);
    }
}
