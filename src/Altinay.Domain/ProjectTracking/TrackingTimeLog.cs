using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Domain.ProjectTracking
{
    public class TrackingTimeLog : AuditedEntity<Guid>
    {
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = false; // Currently running timer

        public TrackingTimeLog() { }

        public TrackingTimeLog(Guid issueId, Guid userId, DateTime startTime, string? description = null)
        {
            IssueId = issueId;
            UserId = userId;
            StartTime = startTime;
            Description = description;
            IsActive = true;
        }

        public TimeSpan? GetDuration()
        {
            if (EndTime.HasValue)
            {
                return EndTime.Value - StartTime;
            }
            return null;
        }

        public TimeSpan GetCurrentDuration()
        {
            var endTime = EndTime ?? DateTime.UtcNow;
            return endTime - StartTime;
        }
    }
}


