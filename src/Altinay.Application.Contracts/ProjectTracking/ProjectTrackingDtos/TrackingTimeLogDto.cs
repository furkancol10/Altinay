using System;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class TrackingTimeLogDto
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
