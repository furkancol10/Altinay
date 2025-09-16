using System;
using Volo.Abp.Domain.Entities;

namespace Altinay.Domain.ProjectTracking
{
    public class TrackingIssueTag : Entity<Guid>
    {
        public Guid IssueId { get; set; }
        public Guid TagId { get; set; }

        public TrackingIssueTag() { }

        public TrackingIssueTag(Guid issueId, Guid tagId)
        {
            IssueId = issueId;
            TagId = tagId;
        }
    }
}



