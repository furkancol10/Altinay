using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinay.Files;
using Altinay.Permissions;
using Altinay.Projects;
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
        private readonly IProjectRepository _projectRepository;
        private readonly IFileRepository _fileRepository;


        public ProjectGroupAppService(
         IProjectGroupRepository projectGroupRepository,
         ProjectGroupManager projectGroupManager,
         IProjectRepository projectRepository,
         IFileRepository fileRepository)
        {
            _projectGroupRepository = projectGroupRepository;
            _projectGroupManager = projectGroupManager;
            _projectRepository = projectRepository;
            _fileRepository = fileRepository;
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
            var projectGroup = await _projectGroupManager.CreateAsync(input.ProjectId, input.FileAliasId);

            // Set GroupName using actual ProjectCode and FileAlias
            await projectGroup.SetGroupNameAsync(_projectRepository, _fileRepository);

            await _projectGroupRepository.InsertAsync(projectGroup);
            return ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(projectGroup);
        }

        public async Task<ProjectGroupDto> UpdateAsync(Guid id, CreateUpdateProjectGroupDto input)
        {
            var projectGroup = await _projectGroupRepository.GetAsync(id);
            projectGroup.ChangeProjectGroup(input.ProjectId, input.FileAliasId);

            // Update GroupName using actual ProjectCode and FileAlias
            await projectGroup.SetGroupNameAsync(_projectRepository, _fileRepository);

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