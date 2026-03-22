using SmartCity.Domain.Entities;

namespace SmartCity.Application.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident?> GetByIdAsync(Guid id);
        Task<IEnumerable<Incident>> GetAllAsync();
        Task AddAsync(Incident incident);
        void Update(Incident incident);
        void Delete(Incident incident);
        Task SaveChangesAsync();
        Task<(IEnumerable<Incident> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);

    }
}
