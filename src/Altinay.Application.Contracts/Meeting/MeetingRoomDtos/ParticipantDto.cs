using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Altinay.Meeting.MeetingRoomDtos
{
    public class ParticipantDto:Entity<Guid>
    {
        public Guid BookingID { get; set; }
        public string Name { get; set; } = string.Empty;
        private ParticipantDto()
        {
        }
        internal ParticipantDto(Guid bookingId, string name) : base(bookingId)
        {
            BookingID = bookingId;
            Name = name;
        }
    }
}
