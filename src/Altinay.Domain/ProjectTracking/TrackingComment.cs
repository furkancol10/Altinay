using System;
using Volo.Abp.Domain.Entities;

namespace Altinay.Domain.ProjectTracking
{
    public class TrackingComment : Entity<Guid>
    {
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }

        public TrackingComment()
        {
            CreationTime = DateTime.UtcNow;
        }

        public TrackingComment(Guid issueId, Guid userId, string content)
        {
            IssueId = issueId;
            UserId = userId;
            Content = content;
            CreationTime = DateTime.UtcNow;
        }
    }
}


