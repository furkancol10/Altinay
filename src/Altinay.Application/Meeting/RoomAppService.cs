using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.IAppServices;
using Altinay.Meeting.MeetingRoomDtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Meeting
{
    public class RoomAppService : CrudAppService<Room,RoomDto,Guid, PagedAndSortedResultRequestDto, CreateUpdateRoomDto>, IRoomAppService
    {
        public RoomAppService(IRepository<Room,Guid> repository) : base(repository)
        {
        }
    }
}
