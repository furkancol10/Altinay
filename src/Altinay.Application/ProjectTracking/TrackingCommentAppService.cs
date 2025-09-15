using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Altinay.ProjectTracking.IAppServices;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Altinay.Domain.ProjectTracking;

namespace Altinay.Application.ProjectTracking
{
    public class TrackingCommentAppService : ApplicationService, ITrackingCommentAppService
    {
        private readonly IRepository<TrackingComment, Guid> _commentRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public TrackingCommentAppService(
            IRepository<TrackingComment, Guid> commentRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<List<TrackingCommentDto>> GetCommentsAsync(Guid issueId)
        {
            var comments = await _commentRepository.GetListAsync(x => x.IssueId == issueId);
            var userIds = comments.Select(x => x.UserId).Distinct().ToList();
            var users = await _userRepository.GetListAsync(x => userIds.Contains(x.Id));

            return comments.Select(comment =>
            {
                var user = users.FirstOrDefault(u => u.Id == comment.UserId);
                return new TrackingCommentDto
                {
                    Id = comment.Id,
                    IssueId = comment.IssueId,
                    UserId = comment.UserId,
                    UserName = user?.UserName ?? "Unknown",
                    Content = comment.Content,
                    CreationTime = comment.CreationTime
                };
            }).OrderBy(x => x.CreationTime).ToList();
        }

        public async Task<TrackingCommentDto> CreateCommentAsync(CreateCommentDto input)
        {
            var comment = new TrackingComment(input.IssueId, input.UserId, input.Content);
            var createdComment = await _commentRepository.InsertAsync(comment);

            var user = await _userRepository.GetAsync(input.UserId);

            return new TrackingCommentDto
            {
                Id = createdComment.Id,
                IssueId = createdComment.IssueId,
                UserId = createdComment.UserId,
                UserName = user.UserName,
                Content = createdComment.Content,
                CreationTime = createdComment.CreationTime
            };
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            await _commentRepository.DeleteAsync(commentId);
        }
    }
}




