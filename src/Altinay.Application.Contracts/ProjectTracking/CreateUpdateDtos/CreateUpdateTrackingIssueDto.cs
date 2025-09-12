using System;
using System.ComponentModel.DataAnnotations;
using Altinay.Domain.ProjectTracking;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateUpdateTrackingIssueDto
    {
        [Required] public Guid ProjectId { get; set; }
        [Required, MaxLength(128)] public string Title { get; set; }
        public string? Description { get; set; }

        [Required] public IssueStatus Status { get; set; } = IssueStatus.Todo;
        [Required] public IssuePriority Priority { get; set; } = IssuePriority.Medium;

        public Guid? AssigneeUserId { get; set; }
        public DateTime? DueDate { get; set; }
        public int? Order { get; set; } 
    }
}
