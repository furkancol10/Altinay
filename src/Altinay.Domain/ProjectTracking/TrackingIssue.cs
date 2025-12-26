using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Altinay.Domain.ProjectTracking
{
 

    public class TrackingIssue : AuditedAggregateRoot<Guid>
    {

        public IssueStatus Status { get; set; }      // <-- Dış enum
        public IssuePriority Priority { get; set; }
        public Guid ProjectId { get; set; }       // hangi projeye bağlı
        public string Title { get; set; }         // kart başlığı
        public string? Description { get; set; }  // opsiyonel
      
        public Guid? AssigneeUserId { get; set; } // atanmış kişi (opsiyonel)
        public DateTime? DueDate { get; set; }    // son tarih
        public int Order { get; set; }            // sütun sırası
        public DateTime? StartedTime { get; set; }
        public DateTime? DoneTime { get; set; }   // Done olduğu an
        
        // Gantt Chart için yeni alanlar
        public DateTime? StartDate { get; set; }      // Görev başlangıç tarihi
        public int Progress { get; set; } = 0;       // Tamamlanma yüzdesi (0-100)
        public int EstimatedDays { get; set; } = 7;  // Tahmini süre (gün)
        public Guid? ParentTaskId { get; set; }      // Ana görev (alt görevler için)
        public string? Dependencies { get; set; }    // Bağımlılıklar (JSON string olarak)
    }
}
