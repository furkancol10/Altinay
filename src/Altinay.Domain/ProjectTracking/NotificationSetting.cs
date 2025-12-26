using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Altinay.Domain.ProjectTracking
{
    public class NotificationSetting : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public bool EnableDailyStartNotifications { get; set; } = true;
        public int DailyStartNotificationTime { get; set; } = 540; // Minutes from midnight (9:00)
        public bool EnableDailyEndNotifications { get; set; } = true;
        public int DailyEndNotificationTime { get; set; } = 1080; // Minutes from midnight (18:00)
        public bool EnableBreakReminders { get; set; } = true;
        public int BreakReminderInterval { get; set; } = 120; // Minutes
        public bool EnableMotivationNotifications { get; set; } = true;
        public int MotivationNotificationCount { get; set; } = 2; // Max per day
        public bool EnableWeekendNotifications { get; set; } = false;
        public bool EnableHolidayNotifications { get; set; } = false;
        public bool SoundEnabled { get; set; } = true;
        public bool VibrationEnabled { get; set; } = true;

        public NotificationSetting() { }

        public NotificationSetting(Guid userId)
        {
            UserId = userId;
        }
    }
}