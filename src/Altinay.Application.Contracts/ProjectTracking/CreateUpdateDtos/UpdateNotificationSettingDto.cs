using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class UpdateNotificationSettingDto
    {
        public bool MorningNotifications { get; set; }
        public bool EveningNotifications { get; set; }
        public bool BreakReminders { get; set; }
        public bool MotivationNotifications { get; set; }
        
        [Range(30, 480)]
        public int BreakReminderInterval { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string WorkStartTime { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(10)]
        public string WorkEndTime { get; set; } = string.Empty;
        
        public bool SoundEnabled { get; set; }
        public bool VibrationEnabled { get; set; }
    }
}