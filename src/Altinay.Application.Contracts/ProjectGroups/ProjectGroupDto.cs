
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Altinay.ProjectGroups
{
    public class ProjectGroupDto : EntityDto<Guid>
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public Guid ProjectId { get; set; }
        public Guid FileAliasId { get; set; }
        public List<IdentityUserDto> People { get; set; } = new();

    }
}
