using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Altinay.Domain.ProjectTracking
{
    public class NotificationSetting : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public bool MorningNotifications { get; set; } = true;
        public bool EveningNotifications { get; set; } = true;
        public bool BreakReminders { get; set; } = true;
        public bool MotivationNotifications { get; set; } = true;
        public int BreakReminderInterval { get; set; } = 120;
        public string WorkStartTime { get; set; } = "09:00";
        public string WorkEndTime { get; set; } = "18:00";
        public bool SoundEnabled { get; set; } = true;
        public bool VibrationEnabled { get; set; } = true;

        public NotificationSetting() { }

        public NotificationSetting(Guid id, Guid userId)
            : base(id)
        {
            UserId = userId;
        }
    }
}
