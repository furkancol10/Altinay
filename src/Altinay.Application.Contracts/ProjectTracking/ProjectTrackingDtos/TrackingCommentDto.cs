using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class TrackingCommentDto : EntityDto<Guid>
    {
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }
    }
}

