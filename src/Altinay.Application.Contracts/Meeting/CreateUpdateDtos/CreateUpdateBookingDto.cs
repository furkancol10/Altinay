using Altinay.Meeting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altinay.Meeting.CreateUpdateDtos
{
    public class CreateUpdateBookingDto
    {
        public Guid RoomID { get; set; }
        public string? BookedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string MeetingTitle { get; set; }
        public string Description { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
