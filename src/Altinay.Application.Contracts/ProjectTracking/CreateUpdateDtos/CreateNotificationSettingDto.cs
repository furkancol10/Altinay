using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateNotificationSettingDto
    {
        [Required]
        public Guid UserId { get; set; }

        public bool EnableDailyStartNotifications { get; set; } = true;
        public bool EnableDailyEndNotifications { get; set; } = true;
        public bool EnableBreakReminders { get; set; } = true;
        public bool EnableMotivationNotifications { get; set; } = true;

        [Range(1, 8)]
        public int BreakReminderInterval { get; set; } = 2;

        [Range(0, 5)]
        public int MotivationNotificationCount { get; set; } = 2;

        public bool EnableWeekendNotifications { get; set; } = false;
        public bool EnableHolidayNotifications { get; set; } = false;

        [Range(0, 1439)]
        public int DailyStartNotificationTime { get; set; } = 540;

        [Range(0, 1439)]
        public int DailyEndNotificationTime { get; set; } = 1080;
    }
}