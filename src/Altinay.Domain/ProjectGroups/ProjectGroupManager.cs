using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using Altinay.ProjectGroups;

namespace Altinay.ProjectGroups
{
    public class ProjectGroupManager : DomainService
    {
        private readonly IProjectGroupRepository _projectGroupRepository;

        public ProjectGroupManager(IProjectGroupRepository projectGroupRepository)
        {
            _projectGroupRepository = projectGroupRepository;
        }

        public async Task<ProjectGroup> CreateAsync(string groupName,Guid projectId, Guid fileAliasId)
        {
            Check.NotNull(projectId, nameof(projectId));
            Check.NotNull(fileAliasId, nameof(fileAliasId));
            var existingProjectGroup = await _projectGroupRepository.FindByProjectIdAndFileAliasIdAsync(
                projectId, fileAliasId);
            if (existingProjectGroup != null)
            {
                throw new Exception($"Project group with project id {projectId} and file alias id {fileAliasId} already exists.");
            }
            return new ProjectGroup(groupName,projectId, fileAliasId);
        }

        public async Task<ProjectGroup> ChangeProjectIdAndFileAliasIdAsync(ProjectGroup projectGroup, Guid newProjectId, Guid newFileAliasId)
        {
            Check.NotNull(projectGroup, nameof(projectGroup));
            Check.NotNull(newProjectId, nameof(newProjectId));
            Check.NotNull(newFileAliasId, nameof(newFileAliasId));
            var existingProjectGroup = await _projectGroupRepository.FindByProjectIdAndFileAliasIdAsync(
                newProjectId, newFileAliasId);
            if (existingProjectGroup != null && existingProjectGroup.Id != projectGroup.Id)
            {
                throw new Exception($"Project group with project id {newProjectId} and file alias id {newFileAliasId} already exists.");
            }
            var updatedProjectGroup = projectGroup.ChangeProjectIdAndFileAliasId(newProjectId, newFileAliasId);
            updatedProjectGroup.UpdateGroupNameFromIds();
            return updatedProjectGroup;
        }
    }
}
