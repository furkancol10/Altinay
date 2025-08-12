using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Projects
{
    public class ProjectAppService :
        CrudAppService<
            Project, // Entity
            ProjectDto, // DTO for output
            Guid, // Primary key type
            PagedAndSortedResultRequestDto, // Paging and sorting input
            CreateUpdateProjectDto>, // Create/Update input DTO
        IProjectAppService
    {
        public ProjectAppService(IRepository<Project, Guid> repository)
            : base(repository)
        {

        }

    }
}
