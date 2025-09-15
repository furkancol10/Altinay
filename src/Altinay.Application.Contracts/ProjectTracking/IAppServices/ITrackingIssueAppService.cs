using Altinay.Domain.ProjectTracking;
using Altinay.Enums;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingIssueAppService
        : ICrudAppService<
            TrackingIssueDto,            // DTO dönen tip
            Guid,                        // Id tipi
            TrackingIssueSearchInput,    // Listeleme girdi modeli
            CreateUpdateTrackingIssueDto // Create/Update girdi modeli

        >


    {
        Task<TrackingIssueDto> ChangeStatusAsync(Guid id, IssueStatus newStatus);
        Task ReorderAsync(IssueReorderInput input);
        Task AssignAsync(Guid id, Guid userId);
        Task UnassignAsync(Guid id);

    }

}
