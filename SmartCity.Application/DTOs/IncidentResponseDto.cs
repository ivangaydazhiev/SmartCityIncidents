
namespace SmartCity.Application.DTOs
{
    public class IncidentResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public LocationDto? Location { get; set; }
    }
}
