using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinay.Meeting.CreateUpdateDtos
{
    public class CreateUpdateParticipantDto
    {
        public Guid BookingID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
