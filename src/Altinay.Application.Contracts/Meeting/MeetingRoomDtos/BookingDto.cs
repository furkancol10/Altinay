using System;
using Volo.Abp.Application.Dtos;
using Altinay.Meeting;
using System.Collections.Generic;

namespace Altinay.Meeting.MeetingRoomDtos
{
    public class BookingDto:AuditedEntityDto<Guid>
    {
        public Guid RoomID { get; set; }
        public String Room { get; set; }
        public string BookedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string MeetingTitle { get; set; }
        public string Participants { get; set; }
    }
}
