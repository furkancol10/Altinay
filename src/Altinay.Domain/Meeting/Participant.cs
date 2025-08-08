using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Altinay.Meeting
{
    public class Participant:Entity<Guid>
    {
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
        
        public string Name { get; set; }
    }
}
