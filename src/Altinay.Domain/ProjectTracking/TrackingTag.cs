using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Domain.ProjectTracking
{
    public class TrackingTag : AuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#007bff"; // Default blue color
        public Guid ProjectId { get; set; } // Tag belongs to a project

        public TrackingTag() { }

        public TrackingTag(string name, string color, Guid projectId)
        {
            Name = name;
            Color = color;
            ProjectId = projectId;
        }
    }
}


