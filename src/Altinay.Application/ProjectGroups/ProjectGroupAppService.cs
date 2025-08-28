using Altinay.Files;
using Altinay.Permissions;
using Altinay.Projects;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Altinay.ProjectGroups
{
    [Authorize(AltinayPermissions.ProjectGroups.Default)]
    public class ProjectGroupAppService : ApplicationService, IProjectGroupAppService
    {
        private readonly IProjectGroupRepository _projectGroupRepository;
        private readonly ProjectGroupManager _projectGroupManager;
        private readonly IProjectRepository _projectRepository;
        private readonly IFileRepository _fileRepository;

        private readonly IIdentityUserAppService _identityUserAppService;

        public ProjectGroupAppService(
            IProjectGroupRepository projectGroupRepository,
            ProjectGroupManager projectGroupManager,
            IProjectRepository projectRepository,
            IFileRepository fileRepository,
            IIdentityUserAppService identityUserAppService // <-- Add this
        )
        {
            _projectGroupRepository = projectGroupRepository;
            _projectGroupManager = projectGroupManager;
            _projectRepository = projectRepository;
            _fileRepository = fileRepository;
            _identityUserAppService = identityUserAppService; // <-- Add this
        }
        public async Task<ProjectGroupDto> GetAsync(Guid id)
        {
            var projectGroup = await _projectGroupRepository.GetAsync(id);
            return ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(projectGroup);
        }
        public async Task AddUserToGroupAsync(Guid projectGroupId, Guid userId)
        {
            var projectGroup = await _projectGroupRepository.GetAsync(projectGroupId);
            if (projectGroup == null)
                throw new EntityNotFoundException($"Project group {projectGroupId} not found.");

            // Prevent adding the same user twice
            if (!projectGroup.Users.Any(u => u.IdentityUserId == userId))
            {
                projectGroup.AddUser(userId);
                await _projectGroupRepository.UpdateAsync(projectGroup);
            }
            // else: Optionally throw or ignore if already present
        }
        public async Task<PagedResultDto<ProjectGroupDto>> GetListAsync(GetProjectGroupListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ProjectGroup.GroupName);
            }

            // Ensure filter is not null to avoid CS8604
            var filter = input.Filter ?? string.Empty;

            var items = await _projectGroupRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                filter
            );

            var totalCount = string.IsNullOrEmpty(input.Filter)
                ? await _projectGroupRepository.CountAsync()
                : await _projectGroupRepository.CountAsync(
                    p => p.GroupName.Contains(input.Filter)
                      || p.ProjectId.ToString().Contains(input.Filter)
                      || p.FileAliasId.ToString().Contains(input.Filter));

            // Gather all user IDs from all groups
            var allUserIds = items.SelectMany(g => g.Users.Select(u => u.IdentityUserId)).Distinct().ToList();

            // Fetch user details in one call
            // Workaround: GetListAsync does not support Ids, so filter by userName/email if possible, or fetch all and filter in memory
            List<IdentityUserDto> userList = new();
            if (allUserIds.Count > 0)
            {
                // Fetch in batches if needed, or fetch all and filter
                var userListResult = await _identityUserAppService.GetListAsync(new Volo.Abp.Identity.GetIdentityUsersInput
                {
                    MaxResultCount = allUserIds.Count > 1000 ? 1000 : allUserIds.Count // adjust as needed
                });
                userList = userListResult.Items.Where(u => allUserIds.Contains(u.Id)).ToList();
            }
            var userDict = userList.ToDictionary(u => u.Id, u => u);

            // Map groups and fill Users property
            var groupDtos = items.Select(g =>
            {
                var dto = ObjectMapper.Map<ProjectGroup, ProjectGroupDto>(g);
                dto.Users = g.Users
                    .Select(u => userDict.TryGetValue(u.IdentityUserId, out var user) ? user : null)
                    .Where(u => u != null)
                    .ToList();
                return dto;
            }).ToList();

            return new PagedResultDto<ProjectGroupDto>(
                totalCount,
                groupDtos
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