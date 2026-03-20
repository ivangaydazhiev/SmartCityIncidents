
namespace SmartCity.Application.Interfaces
{
    public interface ILocationRepository
    {
        Task<bool> ExistAsync(Guid id);
    }
}
