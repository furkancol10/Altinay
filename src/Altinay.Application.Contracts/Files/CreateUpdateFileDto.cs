using Altinay.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinay.Files
{
    public class CreateUpdateFileDto
    {
        [Required]
        public string FileAlias { get; set; }

        [Required]
        public String? FileDescription { get; set; }

        public bool? IsActive { get; set; }

    }
}
