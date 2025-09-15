using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateSmartNotificationDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string NotificationType { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;
    }
}