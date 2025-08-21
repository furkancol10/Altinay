using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinay.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.ProjectGroups
{
    [Authorize(AltinayPermissions.ProjectGroups.Default)]
    public class ProjectGroupAppService : ApplicationService, IProjectGroupAppService
    {
        private readonly IProjectGroupRepository _projectGroupRepository;
        private readonly ProjectGroupManager _projectGroupManager;

        public ProjectGroupAppService(
            IProjectGroupRepository projectGroupRepository,
            ProjectGroupManager projectGroupManager)
        {
            _projectGroupRepository = projectGroupRepository;
            _projectGroupManager = projectGroupManager;
        }

        public async Task<ProjectGroupDto> GetAsync(Guid id)
        {
            var projectGroup = await _projectGroupRepository.GetAsync(id);
            return ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(projectGroup);
        }

        public async Task<PagedResultDto<ProjectGroupDto>> GetListAsync(GetProjectGroupListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ProjectGroup.GroupName);
            }

            var items = await _projectGroupRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _projectGroupRepository.CountAsync()
                : await _projectGroupRepository.CountAsync(
                    p => p.GroupName.Contains(input.Filter)
                      || p.ProjectId.ToString().Contains(input.Filter)
                      || p.FileAliasId.ToString().Contains(input.Filter));

            return new PagedResultDto<ProjectGroupDto>(
                totalCount,
                ObjectMapper.Map<List<ProjectGroup>, List<ProjectGroupDto>>(items)
            );
        }

        public async Task<ProjectGroupDto> CreateAsync(CreateUpdateProjectGroupDto input)
        {
            if (input.ProjectId == Guid.Empty || input.FileAliasId == Guid.Empty)
                throw new BusinessException("ProjectGroup:InvalidInput")
                    .WithData("Details", "ProjectId and FileAliasId are required.");

            var projectGroup = await _projectGroupManager.CreateAsync(
                input.GroupName,
                input.ProjectId,
                input.FileAliasId);

            await _projectGroupRepository.InsertAsync(projectGroup);
            return ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(projectGroup);
        }

        public async Task<ProjectGroupDto> UpdateAsync(Guid id, CreateUpdateProjectGroupDto input)
        {
            var projectGroup = await _projectGroupRepository.GetAsync(id);

            if (input.ProjectId == Guid.Empty || input.FileAliasId == Guid.Empty)
                throw new BusinessException("ProjectGroup:InvalidInput")
                    .WithData("Details", "ProjectId and FileAliasId are required.");

            projectGroup.ProjectId = input.ProjectId;
                
            projectGroup.FileAliasId= input.FileAliasId;
            projectGroup.GroupName = input.GroupName;
            if (!string.IsNullOrWhiteSpace(input.GroupName))
                projectGroup.GroupName = input.GroupName!.Trim();

            await _projectGroupRepository.UpdateAsync(projectGroup);
            return ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(projectGroup);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _projectGroupRepository.GetAsync(id);
            await _projectGroupRepository.DeleteAsync(entity);
        }
    }
}