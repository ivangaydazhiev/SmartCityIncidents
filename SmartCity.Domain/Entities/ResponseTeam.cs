
namespace SmartCity.Domain.Entities
{
    public class ResponseTeam
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TeamType { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
