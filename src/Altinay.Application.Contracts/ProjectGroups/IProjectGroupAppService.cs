using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Altinay.ProjectGroups
{
    public interface IProjectGroupAppService : IApplicationService
    {
        Task<ProjectGroupDto> GetAsync(Guid id);
        Task<PagedResultDto<ProjectGroupDto>> GetListAsync(GetProjectGroupListDto input);
        Task<ProjectGroupDto> CreateAsync(CreateUpdateProjectGroupDto input);
        Task<ProjectGroupDto> UpdateAsync(Guid id, CreateUpdateProjectGroupDto input);
        Task <ProjectGroupDto>AddUserToGroupAsync(Guid projectGroupId, Guid userId);
        Task DeleteAsync(Guid id);


    }
}