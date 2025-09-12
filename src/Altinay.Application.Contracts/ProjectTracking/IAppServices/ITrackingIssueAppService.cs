using System;
using Volo.Abp.Application.Services;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingIssueAppService
        : ICrudAppService<
            TrackingIssueDto,            // DTO dönen tip
            Guid,                        // Id tipi
            TrackingIssueSearchInput,    // Listeleme girdi modeli (filtreler)
            CreateUpdateTrackingIssueDto // Create/Update girdi modeli
        >
    {
        // İleride ekleyeceğiz (drag&drop/atama):
        // Task ChangeStatusAsync(Guid id, IssueStatus newStatus);
        // Task ReorderAsync(Guid projectId, IssueStatus status, List<Guid> orderedIds);
        // Task AssignAsync(Guid id, Guid userId);
        // Task UnassignAsync(Guid id);
    }
}
