using System;
using System.Threading.Tasks;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Volo.Abp.Application.Services;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface INotificationSettingAppService : IApplicationService
    {
        Task<NotificationSettingDto> GetUserSettingsAsync(Guid userId);
        Task<NotificationSettingDto> CreateUserSettingsAsync(CreateNotificationSettingDto input);
        Task<NotificationSettingDto> UpdateUserSettingsAsync(NotificationSettingDto input);
    }
}
