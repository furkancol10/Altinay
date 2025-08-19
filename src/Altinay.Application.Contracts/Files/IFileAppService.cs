using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Altinay.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task<FileDto> GetAsync(string FileAlias);
        Task<PagedResultDto<FileDto>> GetListAsync(GetFileListDto input);
        Task<FileDto> CreateAsync(CreateUpdateFileDto input);
        Task UpdateAsync(string FileAlias, CreateUpdateFileDto input);
        Task DeleteAsync(string File);
        Task DeleteAsync(FileDto file);
    }
}
