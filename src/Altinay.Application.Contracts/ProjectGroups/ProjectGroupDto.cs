
using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.ProjectGroups
{
    public class ProjectGroupDto : EntityDto<Guid>
    {
        public string GroupName { get; set; }
        public string ProjectId { get; set; }
        public string FileAliasId { get; set; }
    }
}
