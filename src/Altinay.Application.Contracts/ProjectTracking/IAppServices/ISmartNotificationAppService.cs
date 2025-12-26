using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Volo.Abp.Application.Services;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ISmartNotificationAppService : IApplicationService
    {
        Task<List<SmartNotificationDto>> GetUserNotificationsAsync(Guid userId);
        Task<SmartNotificationDto> CreateNotificationAsync(CreateSmartNotificationDto input);
        Task MarkAsReadAsync(Guid notificationId);
        Task DeleteNotificationAsync(Guid notificationId);
        Task<int> GetUnreadCountAsync(Guid userId);
    }
}
