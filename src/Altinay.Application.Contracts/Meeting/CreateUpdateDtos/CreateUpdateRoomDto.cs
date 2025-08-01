using Altinay.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Altinay.Meeting.CreateUpdateDtos
{
    public class CreateUpdateRoomDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid FloorID { get; set; }
        [Required]
        public string Floor { get; set; }
        [Required]
        public int Capacity { get; set; }
        public Availibility Availibility { get; set; }
    }
}
