using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Domain.ProjectTracking
{
    public class TrackingAttachment : AuditedEntity<Guid>
    {
        public Guid IssueId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public Guid UploadedByUserId { get; set; }

        public TrackingAttachment() { }

        public TrackingAttachment(Guid issueId, string fileName, string filePath, string contentType, long fileSize, Guid uploadedByUserId)
        {
            IssueId = issueId;
            FileName = fileName;
            FilePath = filePath;
            ContentType = contentType;
            FileSize = fileSize;
            UploadedByUserId = uploadedByUserId;
        }
    }
}


