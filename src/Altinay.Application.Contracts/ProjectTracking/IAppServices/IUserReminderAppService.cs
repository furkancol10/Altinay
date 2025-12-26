using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Volo.Abp.Application.Services;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface IUserReminderAppService : IApplicationService
    {
        Task<List<UserReminderDto>> GetUserRemindersAsync(Guid userId);
        Task<UserReminderDto> CreateReminderAsync(CreateUserReminderDto input);
        Task<UserReminderDto> UpdateReminderAsync(Guid id, CreateUserReminderDto input);
        Task DeleteReminderAsync(Guid id);
        Task MarkAsCompletedAsync(Guid id);
        Task<List<UserReminderDto>> GetActiveRemindersAsync(Guid userId);
        Task<List<UserReminderDto>> GetUpcomingRemindersAsync(Guid userId, int hours = 24);
    }
}
