using System;
using Volo.Abp.Application.Dtos;
using Altinay.Domain.ProjectTracking;
using Altinay.Enums; // IssueStatus, IssuePriority

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class TrackingIssueDto : EntityDto<Guid>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }

        public Guid? AssigneeUserId { get; set; }
        public DateTime? DueDate { get; set; }

        public int Order { get; set; }

        public DateTime? StartedTime { get; set; }
        public DateTime? DoneTime { get; set; }

        public DateTime CreationTime { get; set; }
        
        // Gantt Chart için yeni alanlar
        public DateTime? StartDate { get; set; }      // Görev başlangıç tarihi
        public int Progress { get; set; } = 0;       // Tamamlanma yüzdesi (0-100)
        public int EstimatedDays { get; set; } = 7;  // Tahmini süre (gün)
        public Guid? ParentTaskId { get; set; }      // Ana görev (alt görevler için)
        public string? Dependencies { get; set; }   // Bağımlılıklar (JSON string olarak)
    }
}
