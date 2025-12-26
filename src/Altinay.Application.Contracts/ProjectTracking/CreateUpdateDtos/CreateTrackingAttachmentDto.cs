using System;

namespace Altinay.ProjectTracking.CreateUpdateDtos
{
    public class CreateTrackingAttachmentDto
    {
        public Guid IssueId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public Guid UploadedByUserId { get; set; }
    }
}
