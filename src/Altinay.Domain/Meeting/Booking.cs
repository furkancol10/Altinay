using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Users;

namespace Altinay.Meeting
{
    public class Booking : FullAuditedAggregateRoot<Guid>
    {
        public Guid RoomID { get;  set; }
        public Room Room { get; set; }
        public string MeetingTitle { get;  set; }
        public DateTime StartTime { get;  set; }
        public DateTime EndTime { get;  set; }
        public string BookedBy { get;  set; }
        public string Description { get;  set; }
        public bool AllDay { get; set; }

        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}
