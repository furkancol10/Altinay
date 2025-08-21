
using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.ProjectGroups
{
    public class ProjectGroupDto : EntityDto<Guid>
    {
        public string GroupName { get; set; }
        public Guid ProjectId { get; set; }
        public Guid FileAliasId { get; set; }
    }
}
