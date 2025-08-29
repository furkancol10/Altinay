using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Altinay.Enums;

namespace Altinay.Projects
{
    public class Project : FullAuditedAggregateRoot<Guid>
    {
       
        public String ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string? ProjectDescription { get; set; }

        private Project()
        {

        }

        internal Project(string projectCode, string projectName, string? projectDescription)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            ProjectDescription = projectDescription;
        }
        internal Project ChangeCode(string projectCode)
        {
            if (string.IsNullOrWhiteSpace(projectCode))
            {
                throw new ArgumentException("Project code cannot be null or empty.", nameof(projectCode));
            }
            ProjectCode = projectCode;
            return this;
        }
    }
}
