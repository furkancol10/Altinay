using System;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class SmartNotificationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string NotificationType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsSent { get; set; }
        public bool IsRead { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime CreationTime { get; set; }
    }
}