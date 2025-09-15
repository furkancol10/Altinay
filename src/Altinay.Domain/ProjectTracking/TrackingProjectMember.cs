using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Domain.ProjectTracking
{
    public class TrackingProjectMember : CreationAuditedEntity<Guid> 
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get;  set; }

      
        public string? Role { get; set; }
        public Guid Uid { get; }

        public TrackingProjectMember() { } // ef
        public TrackingProjectMember(Guid id, Guid projectId, Guid userId, string? role = null)
            : base(id)
        {
            ProjectId = projectId;
            UserId = userId;
            Role = role;
        }

        public TrackingProjectMember(Guid id, Guid uid) : base(id)
        {
            Uid = uid;
        }
    }
}
