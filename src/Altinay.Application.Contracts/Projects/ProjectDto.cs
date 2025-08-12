using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Projects
{
  
    public class ProjectDto : EntityDto<Guid>
    {
        public String ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
    }
}
