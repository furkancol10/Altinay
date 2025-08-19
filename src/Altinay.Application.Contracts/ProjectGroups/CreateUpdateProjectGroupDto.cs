using Altinay.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Altinay.ProjectGroups
{
    public class CreateUpdateProjectGroupDto
    {
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public string FileAliasId { get; set; }
        public string GroupName { get; set; }

    }
}
