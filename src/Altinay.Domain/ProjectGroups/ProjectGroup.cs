using Altinay.Files;
using Altinay.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Altinay.ProjectGroups
{
    public class ProjectGroup : FullAuditedAggregateRoot<Guid>
    {
        public string GroupName { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid FileAliasId { get; private set; }
        //collection
        public virtual ICollection<ProjectGroupUser> Users { get; set; } = new List<ProjectGroupUser>();

        //private list
        private List<ProjectGroupUser> _projectGroupUsers = new List<ProjectGroupUser>();

        private ProjectGroup() { }

        internal ProjectGroup(Guid projectId, Guid fileAliasId)
        {
            ProjectId = projectId;
            FileAliasId = fileAliasId;
        }
        
        public void AddUser(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (_projectGroupUsers.Exists(pgu => pgu.IdentityUserId == userId))
            {
                throw new BusinessException("User already exists in the project group.");
            }
            var projectGroupUser = new ProjectGroupUser(this.Id, userId);
            _projectGroupUsers.Add(projectGroupUser);
            Users.Add(projectGroupUser);
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
