using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core; // for OrderBy(string)
using System.Threading.Tasks;
using Altinay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Altinay.ProjectGroups
{
    public class EFCoreProjectGroupRepository
            : EfCoreRepository<AltinayDbContext, ProjectGroup, Guid>, IProjectGroupRepository
    {
        public EFCoreProjectGroupRepository(IDbContextProvider<AltinayDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<ProjectGroup?> FindByProjectIdAndFileAliasIdAsync(Guid projectId, Guid fileAliasId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(pg => pg.ProjectId == projectId && pg.FileAliasId == fileAliasId);
        }

        public async Task<List<ProjectGroup>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var dbSet = await GetDbSetAsync();
            IQueryable<ProjectGroup> query = dbSet.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(pg =>
                    pg.GroupName.Contains(filter));
            }

            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? nameof(ProjectGroup.GroupName) : sorting);
            return await query
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }

}
