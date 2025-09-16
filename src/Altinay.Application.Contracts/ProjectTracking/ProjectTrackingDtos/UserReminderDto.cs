using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class UserReminderDto : AuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime ReminderTime { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public string ReminderType { get; set; } = string.Empty;
        public Guid? RelatedTaskId { get; set; }
    }
}
