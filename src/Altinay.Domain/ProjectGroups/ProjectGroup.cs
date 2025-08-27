using System;
using Volo.Abp.Domain.Entities.Auditing;
using Altinay.Files;
using Altinay.Projects;
using Volo.Abp;
using System.Threading.Tasks;

namespace Altinay.ProjectGroups
{
    public class ProjectGroup : FullAuditedAggregateRoot<Guid>
    {
        public string GroupName { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid FileAliasId { get; private set; }


        private ProjectGroup() { }

        internal ProjectGroup(Guid projectId, Guid fileAliasId)
        {
            ProjectId = projectId;
            FileAliasId = fileAliasId;
        }

        public void ChangeProjectGroup(Guid newProjectId, Guid newFileAliasId)
        {
            if (newProjectId == Guid.Empty) throw new ArgumentException("ProjectId cannot be empty.", nameof(newProjectId));
            if (newFileAliasId == Guid.Empty) throw new ArgumentException("FileAliasId cannot be empty.", nameof(newFileAliasId));

            ProjectId = newProjectId;
            FileAliasId = newFileAliasId;
        }

        // New method: set GroupName from actual ProjectCode and FileAlias
        public async Task SetGroupNameAsync(IProjectRepository projectRepository, IFileRepository fileRepository)
        {
            var project = await projectRepository.GetAsync(ProjectId);
            var file = await fileRepository.GetAsync(FileAliasId);

            if (project == null) throw new BusinessException("Project not found");
            if (file == null) throw new BusinessException("File not found");

            var formattedProjectCode = project.ProjectCode.Replace(".", "");
            GroupName = $"{formattedProjectCode}_{file.FileAlias}";
        }
    }
}
