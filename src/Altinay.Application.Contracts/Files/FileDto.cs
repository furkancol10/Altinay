using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Files
{

    public class FileDto : EntityDto<Guid>
    {
        public string FileAlias { get; set; }
        public bool? IsActive { get; set; }
        public string? FileDescription { get; set; }
    }
}
