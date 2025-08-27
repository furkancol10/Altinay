/*using System;
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

        public async Task<ProjectGroup> CreateAsync(Guid projectId, Guid fileAliasId)
        {
            Check.NotNull(projectId, nameof(projectId));
            Check.NotNull(fileAliasId, nameof(fileAliasId));

            var existingProjectGroup = await _projectGroupRepository.FindByProjectIdAndFileAliasIdAsync(projectId, fileAliasId);
            if (existingProjectGroup != null)
            {
                throw new Exception($"Project group with project id {projectId} and file alias id {fileAliasId} already exists.");
            }

            // Use IDs to generate a simple group name if no other info is available
            var groupName = $"{projectId}_{fileAliasId}";
            return new ProjectGroup( projectId, fileAliasId);
        }

    }
}*/
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Altinay.ProjectGroups;
using Volo.Abp;

namespace Altinay.ProjectGroups
{
    public class ProjectGroupManager : DomainService
    {
        private readonly IProjectGroupRepository _projectGroupRepository;

        public ProjectGroupManager(IProjectGroupRepository projectGroupRepository)
        {
            _projectGroupRepository = projectGroupRepository;
        }

        public async Task<ProjectGroup> CreateAsync(Guid projectId, Guid fileAliasId)
        {
            var existing = await _projectGroupRepository.FindByProjectIdAndFileAliasIdAsync(projectId, fileAliasId);
            

            return new ProjectGroup(projectId, fileAliasId);
        }
    }
}
