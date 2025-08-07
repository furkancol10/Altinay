using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Altinay.Meeting.MeetingRoomDtos
{
    public class AppointmentDto
    {
        public AppointmentDto()
        {
        }

        public AppointmentDto(Guid roomId, string title, DateTime start, DateTime end, string description, bool allDay = false)
        {
            Id = Guid.NewGuid().ToString();
            RoomId = roomId;
            Title = title;
            Start = start;
            End = end;
            Description = description;
            AllDay = allDay;
        }

        public string Id { get; set; }
        public Guid RoomId { get; set; }
        public string BookedBy { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; } = false;
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
