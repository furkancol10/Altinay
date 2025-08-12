using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Altinay.Projects
{
    public interface IProjectAppService :
        ICrudAppService<
            ProjectDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProjectDto>

    {

    }
}
