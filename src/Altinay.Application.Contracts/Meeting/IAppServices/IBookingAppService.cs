using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.MeetingRoomDtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Altinay.Meeting.IAppServices
{
    public interface IBookingAppService : ICrudAppService<BookingDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookingDto>
    {
    }
}
