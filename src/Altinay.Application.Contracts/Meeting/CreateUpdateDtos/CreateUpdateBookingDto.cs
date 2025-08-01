using Altinay.Meeting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altinay.Meeting.CreateUpdateDtos
{
    public class CreateUpdateBookingDto
    {
        [Required]
        public Guid RoomID { get; set; }
        [Required]
        public string BookedBy { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public string MeetingTitle { get; set; }
        [Required]
        public List<string> Participants { get; set; }
    }
}
