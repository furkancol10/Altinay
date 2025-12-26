using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateUserReminderDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        [Required]
        public DateTime ReminderTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string ReminderType { get; set; } = string.Empty;

        public Guid? RelatedTaskId { get; set; }
    }
}
