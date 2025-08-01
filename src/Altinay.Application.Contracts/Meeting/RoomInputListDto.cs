using Altinay.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Meeting
{
    public class RoomInputListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public Guid? RoomID { get; set; }
        public Guid? FloorID { get; set; }
        public int? Capacity { get; set; }
        public Availibility availibility { get; set; }
    }
}
