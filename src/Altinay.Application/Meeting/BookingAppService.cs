using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.IAppServices;
using Altinay.Meeting.MeetingRoomDtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Meeting
{
    public class BookingAppService : CrudAppService<Floor, BookingDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookingDto>, IBookingAppService
    {
        public BookingAppService(IRepository<Floor, Guid> repository) : base(repository)
        {
        }
    }
}
