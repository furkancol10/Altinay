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
        
        // Gantt Chart için yeni alanlar
        public DateTime? StartDate { get; set; }      // Görev başlangıç tarihi
        public int Progress { get; set; } = 0;       // Tamamlanma yüzdesi (0-100)
        public int EstimatedDays { get; set; } = 7;  // Tahmini süre (gün)
        public Guid? ParentTaskId { get; set; }      // Ana görev (alt görevler için)
        public string? Dependencies { get; set; }    // Bağımlılıklar (JSON string olarak) 
    }
}
