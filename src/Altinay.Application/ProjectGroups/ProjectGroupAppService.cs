using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinay.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
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

        public async Task<ProjectGroupDto> GetAsync(Guid projectId, Guid fileId)
        {
            var projectGroup = await _projectGroupRepository.FindByProjectIdAndFileAliasIdAsync(projectId, fileId);
            if (projectGroup == null)
            {
                throw new EntityNotFoundException(typeof(ProjectGroup), $"{projectId} - {fileId}");
            }
            return ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(projectGroup);
        }

        public async Task<PagedResultDto<ProjectGroupDto>> GetListAsync(GetProjectGroupListDto input)
        {
            var items = await _projectGroupRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            var totalCount = input.Filter == null
                ? await _projectGroupRepository.CountAsync()
                : await _projectGroupRepository.CountAsync(
                    p => p.ProjectId.ToString().Contains(input.Filter) || p.FileAliasId.ToString().Contains(input.Filter)
                );
            return new PagedResultDto<ProjectGroupDto>(
                totalCount,
                ObjectMapper.Map<List<ProjectGroup>, List<ProjectGroupDto>>(items)
            );
        }

        public async Task<ProjectGroupDto> CreateAsync(CreateUpdateProjectGroupDto input)
        {
            var projectGroup = await _projectGroupManager.CreateAsync(
                input.GroupName,
                Guid.Parse(input.ProjectId),
                Guid.Parse(input.FileAliasId)
            );
            await _projectGroupRepository.InsertAsync(projectGroup);
            return ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(projectGroup);
        }

        public async Task UpdateAsync(Guid projectId, Guid fileId, CreateUpdateProjectGroupDto input)
        {
            var projectGroup = await _projectGroupRepository.FindByProjectIdAndFileAliasIdAsync(projectId, fileId);
            if (projectGroup == null)
            {
                throw new EntityNotFoundException(typeof(ProjectGroup), $"{projectId} - {fileId}");
            }
            projectGroup = await _projectGroupManager.ChangeProjectIdAndFileAliasIdAsync(
                projectGroup,
                Guid.Parse(input.ProjectId),
                Guid.Parse(input.FileAliasId)
            );
            await _projectGroupRepository.UpdateAsync(projectGroup);
        }

        public async Task DeleteAsync(Guid projectGroupId)
        {
            var projectGroupEntity = await _projectGroupRepository.GetAsync(projectGroupId);
            if (projectGroupEntity == null)
            {
                throw new EntityNotFoundException(typeof(ProjectGroup), projectGroupId);
            }
            await _projectGroupRepository.DeleteAsync(projectGroupEntity);
        }

        public async Task DeleteAsync(ProjectGroupDto projectGroup)
        {
            if (projectGroup == null)
            {
                throw new ArgumentNullException(nameof(projectGroup));
            }
            var projectGroupEntity = await _projectGroupRepository.GetAsync(projectGroup.Id);
            if (projectGroupEntity == null)
            {
                throw new EntityNotFoundException(typeof(ProjectGroup), projectGroup.Id);
            }
            await _projectGroupRepository.DeleteAsync(projectGroupEntity);
        }
    }
}
