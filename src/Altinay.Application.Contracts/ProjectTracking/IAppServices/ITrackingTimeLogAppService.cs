using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingTimeLogAppService
    {
        Task<List<TrackingTimeLogDto>> GetTimeLogsAsync(Guid issueId);
        Task<TrackingTimeLogDto> StartTimerAsync(Guid issueId, Guid userId, string? description = null);
        Task<TrackingTimeLogDto> StopTimerAsync(Guid timeLogId);
        Task<TrackingTimeLogDto> CreateTimeLogAsync(CreateTrackingTimeLogDto input);
        Task DeleteTimeLogAsync(Guid timeLogId);
        Task<TrackingTimeLogDto?> GetActiveTimerAsync(Guid issueId, Guid userId);
    }
}
