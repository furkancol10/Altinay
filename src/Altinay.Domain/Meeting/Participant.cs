using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Meeting
{
    public class Participant : Entity<Guid>
    {
        public Guid BookingID { get; set; }
        public string Name { get; set; } = string.Empty;
        private Participant()
        {
        }
        internal Participant(Guid bookingId, string name): base(bookingId)
        {
            BookingID = bookingId;
            Name = name;
        }
    }
}
