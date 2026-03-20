using Microsoft.EntityFrameworkCore;
using SmartCity.Application.Interfaces;
using SmartCity.Infrastructure.Persistence;

namespace SmartCity.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly SmartCityDbContext _contex;

        public LocationRepository(SmartCityDbContext contex)
        {
            _contex = contex;
        }

        public async Task<bool> ExistAsync(Guid id)
        {

            return await _contex.Locations.AnyAsync(l => l.Id == id);
        }
    }
}
