using Altinay.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Meeting.MeetingRoomDtos
{
    public class RoomDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid FloorID { get; set; }
        public string Floor { get; set; }
        public int Capacity { get; set; }
        public Availibility Availibility { get; set; }
    }
}
