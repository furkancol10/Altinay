using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingProjectAppService
        : ICrudAppService<
            TrackingProjectDto,                 // DTO dönen tip
            Guid,                               // Id tipi
            PagedAndSortedResultRequestDto,     // Listeleme girdi modeli
            CreateUpdateTrackingProjectDto>     // Create/Update 
    {
    }
}
