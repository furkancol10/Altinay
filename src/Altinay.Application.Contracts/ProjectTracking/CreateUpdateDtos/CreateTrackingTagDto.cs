using System;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateTrackingTagDto
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#007bff";
        public Guid ProjectId { get; set; }
    }
}
