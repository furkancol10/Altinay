using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Altinay.Domain.ProjectTracking
{
    public class SmartNotification : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string NotificationType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsSent { get; set; }
        public bool IsRead { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public SmartNotification() { }

        public SmartNotification(Guid id, Guid userId, string notificationType, string title, string message)
            : base(id)
        {
            UserId = userId;
            NotificationType = notificationType;
            Title = title;
            Message = message;
            IsSent = false;
            IsRead = false;
            SentAt = null;
            ReadAt = null;
        }
    }
}
