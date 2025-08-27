
using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.ProjectGroups
{
    public class ProjectGroupDto : EntityDto<Guid>
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }

        public Guid ProjectId { get; set; }
        public string ProjectCode { get; set; } // <-- add this

        public Guid FileAliasId { get; set; }
        public string FileAlias { get; set; } // <-- add this

    }
}
