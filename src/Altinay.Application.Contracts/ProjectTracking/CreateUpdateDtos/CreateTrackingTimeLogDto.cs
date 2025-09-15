using System;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateTrackingTimeLogDto
    {
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
