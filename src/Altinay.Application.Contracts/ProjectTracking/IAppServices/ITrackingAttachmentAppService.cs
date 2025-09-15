using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;

namespace Altinay.ProjectTracking.IAppServices
{
    public interface ITrackingAttachmentAppService
    {
        Task<List<TrackingAttachmentDto>> GetAttachmentsAsync(Guid issueId);
        Task<TrackingAttachmentDto> CreateAttachmentAsync(CreateTrackingAttachmentDto input);
        Task DeleteAttachmentAsync(Guid attachmentId);
        Task<byte[]> DownloadAttachmentAsync(Guid attachmentId);
    }
}
