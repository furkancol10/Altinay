using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core; // for OrderBy(string)
using System.Threading.Tasks;
using Altinay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Altinay.Files
{
    public class EFCoreFileRepository
        : EfCoreRepository<AltinayDbContext, File, Guid>, IFileRepository
    {
        public EFCoreFileRepository(IDbContextProvider<AltinayDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<File?> FindByAliasAsync(string FileAlias)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FileAlias == FileAlias);
        }

        public async Task<List<File>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();

            IQueryable<File> query = dbSet.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(f =>
                    f.FileAlias.Contains(filter) ||
                    (f.FileDescription != null && f.FileDescription.Contains(filter)));
            }

            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? nameof(File.FileAlias) : sorting);

            return await query
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}