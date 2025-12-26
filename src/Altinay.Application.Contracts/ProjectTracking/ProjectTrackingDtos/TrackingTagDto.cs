using System;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class TrackingTagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#007bff";
        public Guid ProjectId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
