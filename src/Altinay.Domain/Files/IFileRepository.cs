using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Files
{
    public interface IFileRepository : IRepository<File, Guid>
    {
        Task<File?> FindByAliasAsync(string FileAlias);

        Task<List<File>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            );
    }

}
