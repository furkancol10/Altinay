using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Altinay.Domain.ProjectTracking
{
    public enum IssueStatus { Todo, InProgress, Review, Done }
    public enum IssuePriority { Low, Medium, High, Critical }

    public class TrackingIssue : AuditedAggregateRoot<Guid>
    {
        public Guid ProjectId { get; set; }       // hangi projeye bağlı
        public string Title { get; set; }         // kart başlığı
        public string? Description { get; set; }  // opsiyonel
        public IssueStatus Status { get; set; }   // durum
        public IssuePriority Priority { get; set; } // öncelik
        public Guid? AssigneeUserId { get; set; } // atanmış kişi (opsiyonel)
        public DateTime? DueDate { get; set; }    // son tarih
        public int Order { get; set; }            // sütun sırası
        public DateTime? StartedTime { get; set; }
        public DateTime? DoneTime { get; set; }   // Done olduğu an
    }
}
