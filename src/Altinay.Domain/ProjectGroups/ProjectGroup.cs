using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Altinay.Enums;
using Altinay.Files;
using Altinay.Projects;
using System.Net.NetworkInformation;


namespace Altinay.ProjectGroups
{
    public class ProjectGroup : FullAuditedAggregateRoot<Guid>
    {
        public string GroupName { get; set; }
        public Guid ProjectId { get; set; } // e.g., "5051"
        public Guid FileAliasId { get; set; }   // e.g., "CMD", "KSN"
       

        private ProjectGroup() { }

        internal ProjectGroup(string GroupName,Guid projectId, Guid fileAliasId)
        {
            if (projectId == Guid.Empty)
            {
                throw new ArgumentException("ProjectId cannot be empty.", nameof(projectId));
            }
            if (fileAliasId == Guid.Empty)
            {
                throw new ArgumentException("FileAliasId cannot be empty.", nameof(fileAliasId));
            }

            ProjectId = projectId;
            FileAliasId = fileAliasId;
            GroupName = GroupName ?? throw new ArgumentNullException(nameof(GroupName), "GroupName cannot be null.");
        }

        internal ProjectGroup ChangeProjectGroup(string groupName,Guid newProjectId, Guid newFileAliasId)
        {
            if (newProjectId == Guid.Empty)
            {
                throw new ArgumentException("ProjectId cannot be empty.", nameof(newProjectId));
            }
            if (newFileAliasId == Guid.Empty)
            {
                throw new ArgumentException("FileAliasId cannot be empty.", nameof(newFileAliasId));
            }
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentException("GroupName cannot be null or empty.", nameof(groupName));
            }
            GroupName = groupName;
            ProjectId = newProjectId;
            FileAliasId = newFileAliasId;
            return this;
        }
        public void UpdateGroupNameFromIds()
        {
            GroupName = $"{ProjectId}-{FileAliasId}";
        }
    }
}
