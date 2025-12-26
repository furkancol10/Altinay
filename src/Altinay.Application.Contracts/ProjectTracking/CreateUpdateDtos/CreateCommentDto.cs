using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateCommentDto
    {
        [Required]
        public Guid IssueId { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;
    }
}

