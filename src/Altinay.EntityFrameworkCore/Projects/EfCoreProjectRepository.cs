using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core; // Ensure this namespace is included for dynamic LINQ support

using System.Text;
using System.Threading.Tasks;
using Altinay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Altinay.Projects
{

    public class EfCoreProjectRepository :
        EfCoreRepository<AltinayDbContext, Project, Guid>,
        IProjectRepository
    {
        public EfCoreProjectRepository(IDbContextProvider<AltinayDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<Project> FindByProjectCodeAsync(string projectCode)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(p => p.ProjectCode == projectCode);
        }
        public async Task<List<Project>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.ProjectName.Contains(filter) || p.ProjectCode.Contains(filter))
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? "ProjectName" : sorting) // Use Dynamic LINQ for runtime sorting
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

    }
    
}