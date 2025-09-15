using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateNotificationSettingDto
    {
        [Required]
        public Guid UserId { get; set; }

        public bool MorningNotifications { get; set; } = true;
        public bool EveningNotifications { get; set; } = true;
        public bool BreakReminders { get; set; } = true;
        public bool MotivationNotifications { get; set; } = true;
        
        [Range(30, 480)]
        public int BreakReminderInterval { get; set; } = 120;
        
        [Required]
        [MaxLength(10)]
        public string WorkStartTime { get; set; } = "09:00";
        
        [Required]
        [MaxLength(10)]
        public string WorkEndTime { get; set; } = "18:00";
        
        public bool SoundEnabled { get; set; } = true;
        public bool VibrationEnabled { get; set; } = true;
    }
}
