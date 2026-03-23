
using SmartCity.Domain.Enums;

namespace SmartCity.Application.DTOs
{
    public class IncidentFilterDto
    {
        public IncidentStatus? Status { get; set; }
        public IncidentType? Type { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
