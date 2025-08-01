using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.IAppServices;
using Altinay.Meeting.MeetingRoomDtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Meeting
{
    public class FloorAppService : CrudAppService<Floor, FloorDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateFloorDto>, IFloorAppService
    {
        public FloorAppService(IRepository<Floor, Guid> repository) : base(repository)
        {
        }
    }
}
