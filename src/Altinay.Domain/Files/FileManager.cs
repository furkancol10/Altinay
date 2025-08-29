using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using Altinay.Files;

namespace Altinay.Files
{
    public class FileManager : DomainService
    {
        private readonly IFileRepository _fileRepository;
        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<File> CreateAsync(string fileAlias, string? fileDescription, bool? isActive)
        {
            Check.NotNullOrWhiteSpace(fileAlias, nameof(fileAlias));

            var existingFile = await _fileRepository.FindByAliasAsync(fileAlias);
            if (existingFile != null)
            {
                throw new Exception($"File with alias {fileAlias} already exists.");
            }
            return new File(fileAlias, fileDescription, isActive);

        }
        public async Task<File> ChangeAlias(File file, string newFileAlias)
        {
            Check.NotNull(file, nameof(file));
            Check.NotNullOrWhiteSpace(newFileAlias, nameof(newFileAlias));
            var existingFile = await _fileRepository.FindByAliasAsync(newFileAlias);
            if (existingFile != null && existingFile.Id != file.Id)
            {
                throw new Exception($"File with alias {newFileAlias} already exists.");
            }
            return file.ChangeAlias(newFileAlias);
        }
      

    }

}
