using Altinay.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinay.Projects
{
    public class CreateUpdateProjectDto 
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public String ProjectCode { get; set; }

        public string? ProjectDescription { get; set; }
        
    }
}
