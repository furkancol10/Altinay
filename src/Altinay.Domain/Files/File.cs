using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Altinay.Enums;
using Altinay.Files;
using System.Net.NetworkInformation;

namespace Altinay.Files
{
    public class File : FullAuditedAggregateRoot<Guid>
    {
        public string FileAlias { get; set; }
        public string? FileDescription { get; set; }
        public bool? IsActive { get; set; } = true;

        private File()
        {

        }
        internal File(string fileAlias, string fileDescription, bool? isActive)
        {
            FileAlias = fileAlias;
            FileDescription = fileDescription;
            IsActive = isActive;
        }
        internal File ChangeAlias(string fileAlias)
        {
            if (string.IsNullOrWhiteSpace(FileAlias))
            {
                throw new ArgumentException("Project code cannot be null or empty.", nameof(FileAlias));
            }
            FileAlias = fileAlias;
            return this;
        }

    }
}
