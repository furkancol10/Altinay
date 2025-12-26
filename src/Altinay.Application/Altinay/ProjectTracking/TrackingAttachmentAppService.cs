using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Altinay.ProjectTracking.IAppServices;
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Altinay.Domain.ProjectTracking;

namespace Altinay.ProjectTracking
{
    public class TrackingAttachmentAppService : ApplicationService, ITrackingAttachmentAppService
    {
        private readonly IRepository<TrackingAttachment, Guid> _attachmentRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public TrackingAttachmentAppService(
            IRepository<TrackingAttachment, Guid> attachmentRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            _attachmentRepository = attachmentRepository;
            _userRepository = userRepository;
        }

        public async Task<List<TrackingAttachmentDto>> GetAttachmentsAsync(Guid issueId)
        {
            var attachments = await _attachmentRepository.GetListAsync(x => x.IssueId == issueId);
            var userIds = attachments.Select(x => x.UploadedByUserId).Distinct().ToList();
            var users = await _userRepository.GetListAsync(x => userIds.Contains(x.Id));

            return attachments.Select(attachment =>
            {
                var user = users.FirstOrDefault(u => u.Id == attachment.UploadedByUserId);
                return new TrackingAttachmentDto
                {
                    Id = attachment.Id,
                    IssueId = attachment.IssueId,
                    FileName = attachment.FileName,
                    FilePath = attachment.FilePath,
                    ContentType = attachment.ContentType,
                    FileSize = attachment.FileSize,
                    UploadedByUserId = attachment.UploadedByUserId,
                    UploadedByUserName = user?.UserName ?? "Unknown",
                    CreationTime = attachment.CreationTime
                };
            }).OrderByDescending(x => x.CreationTime).ToList();
        }

        public async Task<TrackingAttachmentDto> CreateAttachmentAsync(CreateTrackingAttachmentDto input)
        {
            var attachment = new TrackingAttachment(
                input.IssueId,
                input.FileName,
                input.FilePath,
                input.ContentType,
                input.FileSize,
                input.UploadedByUserId
            );
            
            var createdAttachment = await _attachmentRepository.InsertAsync(attachment);
            var user = await _userRepository.GetAsync(input.UploadedByUserId);

            return new TrackingAttachmentDto
            {
                Id = createdAttachment.Id,
                IssueId = createdAttachment.IssueId,
                FileName = createdAttachment.FileName,
                FilePath = createdAttachment.FilePath,
                ContentType = createdAttachment.ContentType,
                FileSize = createdAttachment.FileSize,
                UploadedByUserId = createdAttachment.UploadedByUserId,
                UploadedByUserName = user.UserName,
                CreationTime = createdAttachment.CreationTime
            };
        }

        public async Task DeleteAttachmentAsync(Guid attachmentId)
        {
            await _attachmentRepository.DeleteAsync(attachmentId);
        }

        public async Task<byte[]> DownloadAttachmentAsync(Guid attachmentId)
        {
            var attachment = await _attachmentRepository.GetAsync(attachmentId);
            // TODO: Implement actual file download logic
            // This would typically read from file system or cloud storage
            return new byte[0]; // Placeholder
        }
    }
}
