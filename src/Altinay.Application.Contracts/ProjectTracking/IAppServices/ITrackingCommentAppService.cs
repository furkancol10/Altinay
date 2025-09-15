using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingCommentAppService : IApplicationService
    {
        Task<List<TrackingCommentDto>> GetCommentsAsync(Guid issueId);
        Task<TrackingCommentDto> CreateCommentAsync(CreateCommentDto input);
        Task DeleteCommentAsync(Guid commentId);
    }
}


