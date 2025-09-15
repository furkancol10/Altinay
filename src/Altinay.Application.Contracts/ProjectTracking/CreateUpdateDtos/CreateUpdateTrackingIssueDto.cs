using System;
using System.ComponentModel.DataAnnotations;
using Altinay.Domain.ProjectTracking;
using Altinay.Enums;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateUpdateTrackingIssueDto
    {
        public Guid Id { get; set; }
        [Required] public Guid ProjectId { get; set; }
        [Required, MaxLength(128)] public string Title { get; set; }
        public string? Description { get; set; }

        [Required] public IssueStatus Status { get; set; } = IssueStatus.ToDo;
        [Required] public IssuePriority Priority { get; set; } = IssuePriority.Medium;

        public Guid? AssigneeUserId { get; set; }   // nullable Guid
        public DateTime? DueDate { get; set; }
        public DateTime? StartedTime { get; set; }
        public DateTime? DoneTime { get; set; }
        public int? Order { get; set; } 
    }
}
