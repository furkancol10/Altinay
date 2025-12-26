using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Altinay.ProjectTracking.IAppServices;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Altinay.Domain.ProjectTracking;

namespace Altinay.ProjectTracking
{
    public class TrackingTagAppService : ApplicationService, ITrackingTagAppService
    {
        private readonly IRepository<TrackingTag, Guid> _tagRepository;
        private readonly IRepository<TrackingIssueTag, Guid> _issueTagRepository;

        public TrackingTagAppService(
            IRepository<TrackingTag, Guid> tagRepository,
            IRepository<TrackingIssueTag, Guid> issueTagRepository)
        {
            _tagRepository = tagRepository;
            _issueTagRepository = issueTagRepository;
        }

        public async Task<List<TrackingTagDto>> GetTagsAsync(Guid projectId)
        {
            var tags = await _tagRepository.GetListAsync(x => x.ProjectId == projectId);
            
            return tags.Select(tag => new TrackingTagDto
            {
                Id = tag.Id,
                Name = tag.Name,
                Color = tag.Color,
                ProjectId = tag.ProjectId,
                CreationTime = tag.CreationTime
            }).OrderBy(x => x.Name).ToList();
        }

        public async Task<List<TrackingTagDto>> GetIssueTagsAsync(Guid issueId)
        {
            var issueTags = await _issueTagRepository.GetListAsync(x => x.IssueId == issueId);
            var tagIds = issueTags.Select(x => x.TagId).ToList();
            
            if (!tagIds.Any())
                return new List<TrackingTagDto>();

            var tags = await _tagRepository.GetListAsync(x => tagIds.Contains(x.Id));
            
            return tags.Select(tag => new TrackingTagDto
            {
                Id = tag.Id,
                Name = tag.Name,
                Color = tag.Color,
                ProjectId = tag.ProjectId,
                CreationTime = tag.CreationTime
            }).OrderBy(x => x.Name).ToList();
        }

        public async Task<TrackingTagDto> CreateTagAsync(CreateTrackingTagDto input)
        {
            var tag = new TrackingTag(input.Name, input.Color, input.ProjectId);
            var createdTag = await _tagRepository.InsertAsync(tag);

            return new TrackingTagDto
            {
                Id = createdTag.Id,
                Name = createdTag.Name,
                Color = createdTag.Color,
                ProjectId = createdTag.ProjectId,
                CreationTime = createdTag.CreationTime
            };
        }

        public async Task DeleteTagAsync(Guid tagId)
        {
            // First remove all issue-tag relationships
            var issueTags = await _issueTagRepository.GetListAsync(x => x.TagId == tagId);
            foreach (var issueTag in issueTags)
            {
                await _issueTagRepository.DeleteAsync(issueTag);
            }
            
            // Then delete the tag
            await _tagRepository.DeleteAsync(tagId);
        }

        public async Task AssignTagToIssueAsync(Guid issueId, Guid tagId)
        {
            // Check if relationship already exists
            var existing = await _issueTagRepository.FirstOrDefaultAsync(x => x.IssueId == issueId && x.TagId == tagId);
            if (existing == null)
            {
                var issueTag = new TrackingIssueTag(issueId, tagId);
                await _issueTagRepository.InsertAsync(issueTag);
            }
        }

        public async Task RemoveTagFromIssueAsync(Guid issueId, Guid tagId)
        {
            var issueTag = await _issueTagRepository.FirstOrDefaultAsync(x => x.IssueId == issueId && x.TagId == tagId);
            if (issueTag != null)
            {
                await _issueTagRepository.DeleteAsync(issueTag);
            }
        }
    }
}
