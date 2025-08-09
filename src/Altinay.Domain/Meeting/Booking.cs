using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Meeting
{
    public class Booking : FullAuditedAggregateRoot<Guid>
    {
        public Guid RoomID { get;  set; }
        public string? BookedBy { get;  set; }
        public DateTime StartTime { get;  set; }
        public DateTime EndTime { get;  set; }
        public string MeetingTitle { get;  set; }
        public string Participants { get;  set; }
    }
}
