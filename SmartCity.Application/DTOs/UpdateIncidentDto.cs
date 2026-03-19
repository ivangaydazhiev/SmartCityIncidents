
namespace SmartCity.Application.DTOs
{
    public class UpdateIncidentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Type { get; set; }
        public int Status { get; set; }
        public Guid LocationId { get; set; }
    }
}
