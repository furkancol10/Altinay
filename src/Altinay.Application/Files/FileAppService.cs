using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinay.Files;
using Altinay.Permissions;
using Altinay.Projects;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Files
{
    [Authorize(AltinayPermissions.Files.Default)]
    public class FileAppService : ApplicationService, IFileAppService
    {
        private readonly IFileRepository _fileRepository;
        private readonly FileManager _fileManager;

        public FileAppService(
            IFileRepository fileRepository,
            FileManager fileManager)
        {
            _fileRepository = fileRepository;
            _fileManager = fileManager;
        }
        public async Task<FileDto> GetAsync(string fileAlias)
        {
            var file = await _fileRepository.FindByAliasAsync(fileAlias);
            return ObjectMapper.Map<File, FileDto>(file);
        }


        public async Task<FileDto> CreateAsync(CreateUpdateFileDto input)
        {
            var file = await _fileManager.CreateAsync(
                input.FileAlias,
                input.FileDescription,
                input.IsActive
            );

            await _fileRepository.InsertAsync(file);
            return ObjectMapper.Map<File, FileDto>(file);
        }

        public async Task<PagedResultDto<FileDto>> GetListAsync(GetFileListDto input)
        {
            var items = await _fileRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _fileRepository.CountAsync()
                : await _fileRepository.CountAsync(p => p.FileAlias.Contains(input.Filter));

            return new PagedResultDto<FileDto>(
                totalCount,
                ObjectMapper.Map<List<File>, List<FileDto>>(items)
            );
        }
        //AltinayPermissions.Projects.Create

        [Authorize(AltinayPermissions.Projects.Update)]
        public async Task UpdateAsync(string fileAlias, CreateUpdateFileDto input)
        {
            var file = await _fileRepository.FindByAliasAsync(fileAlias);
            /* if (file == null)
             {
                 throw new EntityNotFoundException(typeof(File), fileAlias);
             }
             file = await _fileManager.ChangeAlias(file, input.FileAlias);*/
            file.FileDescription = input.FileDescription;
            file.IsActive = input.IsActive;
            await _fileRepository.UpdateAsync(file);
        }

        [Authorize(AltinayPermissions.Projects.Delete)]

        public async Task DeleteAsync(String fileAlias)
        {
            var file = await _fileRepository.FindByAliasAsync(fileAlias);
            if (file != null)
            {
                await _fileRepository.DeleteAsync(file);
            }
        }

        public Task DeleteAsync(FileDto file)
        {
            throw new NotImplementedException();
        }
    }
}
        