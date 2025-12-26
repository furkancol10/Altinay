using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Altinay.ProjectTracking.IAppServices;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;

namespace Altinay.Controllers
{
    [Route("api/tracking-comments")]
    public class TrackingCommentController : AbpControllerBase
    {
        private readonly ITrackingCommentAppService _commentAppService;

        public TrackingCommentController(ITrackingCommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }

        [HttpGet("issue/{issueId}")]
        public async Task<List<TrackingCommentDto>> GetCommentsAsync(Guid issueId)
        {
            return await _commentAppService.GetCommentsAsync(issueId);
        }

        [HttpPost]
        public async Task<TrackingCommentDto> CreateCommentAsync([FromBody] CreateCommentDto input)
        {
            return await _commentAppService.CreateCommentAsync(input);
        }

        [HttpDelete("{commentId}")]
        public async Task DeleteCommentAsync(Guid commentId)
        {
            await _commentAppService.DeleteCommentAsync(commentId);
        }
    }
}


