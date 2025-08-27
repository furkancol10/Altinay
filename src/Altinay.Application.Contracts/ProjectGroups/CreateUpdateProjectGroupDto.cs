using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectGroups
{
    public class CreateUpdateProjectGroupDto
    {
        [Required(ErrorMessage = "Project is required")]
        public Guid ProjectId { get; set; }

        // Keep this if you still want to support single selection (dropdown)
        public Guid FileAliasId { get; set; }

        // Add this for multiple selection (checkboxes)
        [Required(ErrorMessage = "At least one file alias is required")]
        public List<Guid> FileAliasIds { get; set; } = new();
    }
}
