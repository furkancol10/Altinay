using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;        // <-- Bunu mutlaka ekle
using Volo.Abp.Application.Services;

using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using System.Collections.Generic;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingProjectAppService
        : ICrudAppService<
            TrackingProjectDto,                 // read DTO
            Guid,                               // id
            PagedAndSortedResultRequestDto,     // list input
            CreateUpdateTrackingProjectDto>     // create/update input
    {
        
        Task<PagedResultDto<LookupDto<Guid>>> GetMembersAsync(Guid projectId);
        Task<PagedResultDto<LookupDto<Guid>>> GetAllUsersAsync();
        Task SetMembersAsync(Guid projectId, List<Guid> userIds);


        // Projeye üyeleri topluca set et (seçtiklerin kalsın, diğerleri çıksın)

    }

    public class LookupDto<T>
    {
        public string DisplayName { get; set; }
        public Guid Id { get; set; }
    }
}
