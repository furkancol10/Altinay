using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.MeetingRoomDtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Altinay.Meeting.IAppServices
{
    public interface IRoomAppService : ICrudAppService<RoomDto, Guid, RoomInputListDto,CreateUpdateRoomDto>
    {

    }
}
