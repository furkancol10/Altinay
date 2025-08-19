using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace Altinay.ProjectGroups
{
    public interface IProjectGroupAppService : IApplicationService
    {
        Task<ProjectGroupDto> GetAsync(Guid projectId, Guid fileId);
        Task<PagedResultDto<ProjectGroupDto>> GetListAsync(GetProjectGroupListDto input);
        Task<ProjectGroupDto> CreateAsync(CreateUpdateProjectGroupDto input);
        Task UpdateAsync(Guid projectId, Guid fileId, CreateUpdateProjectGroupDto input);
        Task DeleteAsync(Guid projectGroupId);
        Task DeleteAsync(ProjectGroupDto projectGroup);
    }
    
}
