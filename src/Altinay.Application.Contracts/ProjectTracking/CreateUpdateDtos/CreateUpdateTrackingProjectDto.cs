using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateUpdateTrackingProjectDto
    {
        public Guid Id { get; set; }
        [Required, MaxLength(128)] public string Name { get; set; }         
        [Required, MaxLength(8)] public string Key { get; set; }       
        public string? Description { get; set; } 
    }
}
