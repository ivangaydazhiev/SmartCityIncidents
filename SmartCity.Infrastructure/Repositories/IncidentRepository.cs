using Microsoft.EntityFrameworkCore;
using SmartCity.Application.Interfaces;
using SmartCity.Domain.Entities;
using SmartCity.Infrastructure.Persistence;

namespace SmartCity.Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly SmartCityDbContext _context;

        public IncidentRepository(SmartCityDbContext context)
        {
            _context = context;
        }

        public async Task<Incident?> GetByIdAsync(Guid id)
        {
            return await _context.Incidents
                .Include(i => i.Location)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Incident>> GetAllAsync()
        {
            return await _context.Incidents
                .Include(i => i.Location)
                .ToListAsync();
        }

        public async Task AddAsync(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
        }

        public void Update(Incident incident)
        {
             _context.Incidents.Update(incident);
        }

        public void Delete(Incident incident)
        {
            _context.Incidents.Remove(incident);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Incident> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Incidents
                .Include(i => i.Location)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
