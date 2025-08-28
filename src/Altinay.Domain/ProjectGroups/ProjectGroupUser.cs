using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;


namespace Altinay.ProjectGroups
{
    public class ProjectGroupUser : Entity
    {
        public Guid Id { get; private set; }
        public Guid ProjectGroupId { get; private set; }

        public virtual ProjectGroup ProjectGroup { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }

        protected ProjectGroupUser() { }

        public ProjectGroupUser(Guid projectGroupId, Guid userId)
        {
            ProjectGroupId = projectGroupId;
            Id = userId;
        }

        public override object?[] GetKeys()
        {
            return new object?[] { ProjectGroupId, Id };
        }
    }
}
