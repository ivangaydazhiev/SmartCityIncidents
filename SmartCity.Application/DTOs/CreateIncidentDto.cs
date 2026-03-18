using SmartCity.Domain.Enums;

namespace SmartCity.Application.DTOs
{
    public class CreateIncidentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IncidentType Type {  get; set; }
        public Guid LocationId { get; set; }
    }
}
