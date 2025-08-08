using Altinay.Meeting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altinay.Meeting.CreateUpdateDtos
{
    public class CreateUpdateBookingDto
    {
        public Guid RoomID { get; set; }
        public Room Room{ get; set; }
        public string MeetingTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string BookedBy { get; set; }
        public string Description { get; set; }
        public bool AllDay { get; set; }

        public List<string> Participant { get; set; }
    }
}
