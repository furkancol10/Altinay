using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectGroups
{
    public class CreateUpdateProjectGroupDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid FileAliasId { get; set; }

        public string? GroupName { get; set; }=string.Empty;
    }
}