using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.IAppServices;
using Altinay.Meeting.MeetingRoomDtos;
using Altinay.Personel.Managers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Meeting
{
    public class RoomAppService : CrudAppService<Room, RoomDto, Guid, RoomInputListDto, CreateUpdateRoomDto>, IRoomAppService
    {
        private readonly IRepository<Room, Guid> _roomRepository;

        public RoomAppService(IRepository<Room, Guid> roomRepository)
            : base(roomRepository)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<IQueryable<Room>> CreateFilteredQueryAsync(RoomInputListDto input)
        {
            var query = await _roomRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(x => x.Name.Contains(input.Filter));
            }

            if (input.FloorID.HasValue)
            {
                query = query.Where(x => x.FloorID == input.FloorID.Value);
            }

            if (input.Capacity.HasValue)
            {
                query = query.Where(x => x.Capacity >= input.Capacity.Value);
            }

            return query;
        }
    }
}
