using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Meeting
{
    public class BookingInputListDto : PagedAndSortedResultRequestDto
    {
        public Guid? RoomID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Filter { get; set; }
    }
}
