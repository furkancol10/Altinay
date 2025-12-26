using System;
using Volo.Abp.Application.Dtos;
using Altinay.Domain.ProjectTracking;
using Altinay.Enums;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class TrackingIssueSearchInput : PagedAndSortedResultRequestDto
    {
        public Guid? ProjectId { get; set; }
        public IssueStatus? Status { get; set; }
        public Guid? AssigneeUserId { get; set; }
        public string? Text { get; set; }
        public DateTime? DueFrom { get; set; }
        public DateTime? DueTo { get; set; }
        public string? Filter { get; set; }
    }
}
