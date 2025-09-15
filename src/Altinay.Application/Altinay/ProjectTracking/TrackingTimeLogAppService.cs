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
    public class TrackingTimeLogAppService : ApplicationService, ITrackingTimeLogAppService
    {
        private readonly IRepository<TrackingTimeLog, Guid> _timeLogRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public TrackingTimeLogAppService(
            IRepository<TrackingTimeLog, Guid> timeLogRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            _timeLogRepository = timeLogRepository;
            _userRepository = userRepository;
        }

        public async Task<List<TrackingTimeLogDto>> GetTimeLogsAsync(Guid issueId)
        {
            var timeLogs = await _timeLogRepository.GetListAsync(x => x.IssueId == issueId);
            var userIds = timeLogs.Select(x => x.UserId).Distinct().ToList();
            var users = await _userRepository.GetListAsync(x => userIds.Contains(x.Id));

            return timeLogs.Select(timeLog =>
            {
                var user = users.FirstOrDefault(u => u.Id == timeLog.UserId);
                return new TrackingTimeLogDto
                {
                    Id = timeLog.Id,
                    IssueId = timeLog.IssueId,
                    UserId = timeLog.UserId,
                    UserName = user?.UserName ?? "Unknown",
                    StartTime = timeLog.StartTime,
                    EndTime = timeLog.EndTime,
                    Description = timeLog.Description,
                    IsActive = timeLog.IsActive,
                    Duration = timeLog.GetDuration(),
                    CreationTime = timeLog.CreationTime
                };
            }).OrderByDescending(x => x.StartTime).ToList();
        }

        public async Task<TrackingTimeLogDto> StartTimerAsync(Guid issueId, Guid userId, string? description = null)
        {
            // Stop any active timer for this user and issue
            var activeTimer = await GetActiveTimerAsync(issueId, userId);
            if (activeTimer != null)
            {
                await StopTimerAsync(activeTimer.Id);
            }

            var timeLog = new TrackingTimeLog(issueId, userId, DateTime.UtcNow, description);
            var createdTimeLog = await _timeLogRepository.InsertAsync(timeLog);
            var user = await _userRepository.GetAsync(userId);

            return new TrackingTimeLogDto
            {
                Id = createdTimeLog.Id,
                IssueId = createdTimeLog.IssueId,
                UserId = createdTimeLog.UserId,
                UserName = user.UserName,
                StartTime = createdTimeLog.StartTime,
                EndTime = createdTimeLog.EndTime,
                Description = createdTimeLog.Description,
                IsActive = createdTimeLog.IsActive,
                Duration = createdTimeLog.GetDuration(),
                CreationTime = createdTimeLog.CreationTime
            };
        }

        public async Task<TrackingTimeLogDto> StopTimerAsync(Guid timeLogId)
        {
            var timeLog = await _timeLogRepository.GetAsync(timeLogId);
            timeLog.EndTime = DateTime.UtcNow;
            timeLog.IsActive = false;
            
            var updatedTimeLog = await _timeLogRepository.UpdateAsync(timeLog);
            var user = await _userRepository.GetAsync(timeLog.UserId);

            return new TrackingTimeLogDto
            {
                Id = updatedTimeLog.Id,
                IssueId = updatedTimeLog.IssueId,
                UserId = updatedTimeLog.UserId,
                UserName = user.UserName,
                StartTime = updatedTimeLog.StartTime,
                EndTime = updatedTimeLog.EndTime,
                Description = updatedTimeLog.Description,
                IsActive = updatedTimeLog.IsActive,
                Duration = updatedTimeLog.GetDuration(),
                CreationTime = updatedTimeLog.CreationTime
            };
        }

        public async Task<TrackingTimeLogDto> CreateTimeLogAsync(CreateTrackingTimeLogDto input)
        {
            var timeLog = new TrackingTimeLog(input.IssueId, input.UserId, input.StartTime, input.Description)
            {
                EndTime = input.EndTime,
                IsActive = input.IsActive
            };
            
            var createdTimeLog = await _timeLogRepository.InsertAsync(timeLog);
            var user = await _userRepository.GetAsync(input.UserId);

            return new TrackingTimeLogDto
            {
                Id = createdTimeLog.Id,
                IssueId = createdTimeLog.IssueId,
                UserId = createdTimeLog.UserId,
                UserName = user.UserName,
                StartTime = createdTimeLog.StartTime,
                EndTime = createdTimeLog.EndTime,
                Description = createdTimeLog.Description,
                IsActive = createdTimeLog.IsActive,
                Duration = createdTimeLog.GetDuration(),
                CreationTime = createdTimeLog.CreationTime
            };
        }

        public async Task DeleteTimeLogAsync(Guid timeLogId)
        {
            await _timeLogRepository.DeleteAsync(timeLogId);
        }

        public async Task<TrackingTimeLogDto?> GetActiveTimerAsync(Guid issueId, Guid userId)
        {
            var timeLog = await _timeLogRepository.FirstOrDefaultAsync(x => x.IssueId == issueId && x.UserId == userId && x.IsActive);
            
            if (timeLog == null)
                return null;

            var user = await _userRepository.GetAsync(userId);

            return new TrackingTimeLogDto
            {
                Id = timeLog.Id,
                IssueId = timeLog.IssueId,
                UserId = timeLog.UserId,
                UserName = user.UserName,
                StartTime = timeLog.StartTime,
                EndTime = timeLog.EndTime,
                Description = timeLog.Description,
                IsActive = timeLog.IsActive,
                Duration = timeLog.GetCurrentDuration(),
                CreationTime = timeLog.CreationTime
            };
        }
    }
}
