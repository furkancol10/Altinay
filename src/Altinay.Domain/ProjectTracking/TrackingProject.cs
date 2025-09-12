using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Altinay.Domain.ProjectTracking
{
    public class TrackingProject : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }          // Proje adı
        public string Key { get; set; }           // Kısa kod (örn: ALT)
        public string? Description { get; set; }  // Açıklama (opsiyonel)
        public Guid OwnerUserId { get; set; } // Projeyi oluşturan

    }
}
