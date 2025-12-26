using System;
using System.Collections.Generic;
using Altinay.Domain.ProjectTracking;
using Altinay.Enums;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class IssueReorderInput
    {
        public Guid ProjectId { get; set; }
        public IssueStatus Status { get; set; }
        public List<Guid> OrderedIds { get; set; } = new();
    }
}
