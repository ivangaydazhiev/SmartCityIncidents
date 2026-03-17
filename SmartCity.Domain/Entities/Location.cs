
namespace SmartCity.Domain.Entities
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
