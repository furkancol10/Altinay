using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;


namespace Altinay.ProjectGroups
{
    public class ProjectGroupUser : Entity
    {
        public Guid IdentityUserId { get; private set; }

        // Add a property to map to ABP's "Id" for IdentityUser
        public Guid Id
        {
            get => IdentityUserId;
            private set => IdentityUserId = value;
        }
        public Guid ProjectGroupId { get; private set; }

        public virtual ProjectGroup ProjectGroup { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }

        protected ProjectGroupUser() { }

        public ProjectGroupUser(Guid projectGroupId, Guid userId)
        {
            ProjectGroupId = projectGroupId;
            IdentityUserId = userId;
        }

        public override object?[] GetKeys()
        {
            return new object?[] { ProjectGroupId, IdentityUserId };
        }
    }
}
