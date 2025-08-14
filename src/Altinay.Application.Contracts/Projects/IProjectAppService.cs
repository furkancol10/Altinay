using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Altinay.Projects
{
    public interface IProjectAppService : IApplicationService
    {
      Task<ProjectDto> GetAsync(string projectCode);
        Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectListDto input);
        Task <ProjectDto> CreateAsync(CreateUpdateProjectDto input);
        Task UpdateAsync(string projectCode, CreateUpdateProjectDto input);
        Task DeleteAsync(string projectCode);

    }
}
