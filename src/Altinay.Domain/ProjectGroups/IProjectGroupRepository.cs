using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Altinay.ProjectGroups
{
    public interface IProjectGroupRepository: IRepository<ProjectGroup, Guid>
    {
        Task<ProjectGroup?> FindByProjectIdAndFileAliasIdAsync(Guid projectId, Guid fileAliasId);
        Task<List<ProjectGroup>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
