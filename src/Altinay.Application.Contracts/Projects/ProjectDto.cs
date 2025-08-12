using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Altinay.Projects
{
  
    public class ProjectDto : EntityDto<Guid>
    {
   
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
    }
}
