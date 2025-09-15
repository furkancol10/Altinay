using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingTagAppService
    {
        Task<List<TrackingTagDto>> GetTagsAsync(Guid projectId);
        Task<List<TrackingTagDto>> GetIssueTagsAsync(Guid issueId);
        Task<TrackingTagDto> CreateTagAsync(CreateTrackingTagDto input);
        Task DeleteTagAsync(Guid tagId);
        Task AssignTagToIssueAsync(Guid issueId, Guid tagId);
        Task RemoveTagFromIssueAsync(Guid issueId, Guid tagId);
    }
}
