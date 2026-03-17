using SmartCity.Domain.Enums;

namespace SmartCity.Domain.Entities
{
    public class Incident
    {
        public Guid Id { get; set; }
        public String Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public IncidentType Type { get; set; }
        public IncidentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;

    }
}
