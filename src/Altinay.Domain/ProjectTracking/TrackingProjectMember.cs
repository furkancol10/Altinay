using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Domain.ProjectTracking
{
    // Kullanıcı proje üyeliği
    public class TrackingProjectMember : AuditedAggregateRoot<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }    
    }
}
