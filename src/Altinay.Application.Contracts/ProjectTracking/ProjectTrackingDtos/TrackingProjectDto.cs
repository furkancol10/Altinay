using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class TrackingProjectDto : EntityDto<Guid>
    {
        public string Name { get; set; }       
        public string Key { get; set; }        
        public string? Description { get; set; }
    }
}
