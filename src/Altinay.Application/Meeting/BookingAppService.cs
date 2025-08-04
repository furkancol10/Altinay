using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.IAppServices;
using Altinay.Meeting.MeetingRoomDtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;

namespace Altinay.Meeting
{
    public class BookingAppService : CrudAppService<Booking, BookingDto, Guid, BookingInputListDto, CreateUpdateBookingDto>, IBookingAppService
    {
        public BookingAppService(IRepository<Booking, Guid> repository) : base(repository)
        {
        }

        protected override async Task<IQueryable<Booking>> CreateFilteredQueryAsync(BookingInputListDto input)
        {
            var query = await Repository.GetQueryableAsync();

            if (input.RoomID.HasValue)
            {
                query = query.Where(x => x.RoomID == input.RoomID.Value);
            }

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(x =>
                    x.MeetingTitle.Contains(input.Filter) ||
                    x.BookedBy.Contains(input.Filter));
            }

            if (input.StartTime.HasValue)
            {
                query = query.Where(x => x.StartTime >= input.StartTime.Value);
            }

            if (input.EndTime.HasValue)
            {
                query = query.Where(x => x.EndTime <= input.EndTime.Value);
            }

            return query;
        }
    }
}
