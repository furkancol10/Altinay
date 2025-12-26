using System;

namespace Altinay.ProjectTracking.ProjectTrackingDtos
{
    public class TrackingAttachmentDto
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public Guid UploadedByUserId { get; set; }
        public string UploadedByUserName { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }
    }
}
