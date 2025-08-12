using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Altinay.Enums;

namespace Altinay.Projects
{
    public class Project : FullAuditedAggregateRoot<Guid>
    {
       
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
  
    }
}
