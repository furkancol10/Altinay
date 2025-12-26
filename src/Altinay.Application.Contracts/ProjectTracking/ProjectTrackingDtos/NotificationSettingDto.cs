using System;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class NotificationSettingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool MorningNotifications { get; set; }
        public bool EveningNotifications { get; set; }
        public bool BreakReminders { get; set; }
        public bool MotivationNotifications { get; set; }
        public int BreakReminderInterval { get; set; }
        public string WorkStartTime { get; set; } = string.Empty;
        public string WorkEndTime { get; set; } = string.Empty;
        public bool SoundEnabled { get; set; }
        public bool VibrationEnabled { get; set; }
        public DateTime CreationTime { get; set; }
    }
}