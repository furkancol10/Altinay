using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Meeting
{
    public class FloorInputListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public Guid? FloorID { get; set; } 
    }
}
