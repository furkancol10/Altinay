using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.MeetingRoomDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Altinay.Meeting.IAppServices;

public interface IBookingAppService : ICrudAppService<BookingDto, Guid, BookingInputListDto, CreateUpdateBookingDto>
{
    //Task CreateAppointmentAsync(List<AppointmentDto> input);
}
