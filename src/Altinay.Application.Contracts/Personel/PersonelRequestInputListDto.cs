using Altinay.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Personel
{
    public class PersonelRequestInputListDto:PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public RequestType? RequestType { get; set; }
        public RequestStatus? RequestStatus { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
