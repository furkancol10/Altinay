using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Domain.ProjectTracking
{
    public class UserReminder : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime ReminderTime { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public string ReminderType { get; set; } = string.Empty;
        public Guid? RelatedTaskId { get; set; }

        public UserReminder() { }

        public UserReminder(
            Guid userId,
            string title,
            string message,
            DateTime reminderTime,
            string reminderType,
            Guid? relatedTaskId = null)
        {
            UserId = userId;
            Title = title;
            Message = message;
            ReminderTime = reminderTime;
            ReminderType = reminderType;
            RelatedTaskId = relatedTaskId;
            IsCompleted = false;
            IsActive = true;
        }
    }
}