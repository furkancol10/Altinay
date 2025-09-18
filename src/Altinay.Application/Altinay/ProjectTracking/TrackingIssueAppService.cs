using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Authorization;

using Altinay.Domain.ProjectTracking;                    // TrackingIssue + enums
using Altinay.ProjectTracking.IAppServices;              // ITrackingIssueAppService (Contracts)
using Altinay.ProjectTracking.ProjectTrackingDtos;       // TrackingIssueDto, TrackingIssueSearchInput
using Altinay.ProjectTracking.CreateUpdateDtos;          // CreateUpdateTrackingIssueDto
using Altinay.Permissions;

namespace Altinay.ProjectTracking
{
    public class TrackingIssueAppService :
        CrudAppService<
            TrackingIssue,                      // Entity
            TrackingIssueDto,                   // Read DTO
            Guid,                               // Id
            TrackingIssueSearchInput,           // List input
            CreateUpdateTrackingIssueDto,       // Create input
            CreateUpdateTrackingIssueDto        // Update input
        >,
        ITrackingIssueAppService
    {
        public TrackingIssueAppService(IRepository<TrackingIssue, Guid> repo)
            : base(repo)
        {
        }

        public override async Task<PagedResultDto<TrackingIssueDto>> GetListAsync(TrackingIssueSearchInput input)
        {
            var query = await Repository.GetQueryableAsync();

            if (input.ProjectId.HasValue)
                query = query.Where(x => x.ProjectId == input.ProjectId.Value);

            if (input.Status.HasValue)
                query = query.Where(x => x.Status == input.Status.Value);

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                var f = input.Filter.Trim();
                query = query.Where(x => x.Title.Contains(f));
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Status)
                .ThenBy(x => x.Order)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var dtos = ObjectMapper.Map<List<TrackingIssue>, List<TrackingIssueDto>>(items);
            return new PagedResultDto<TrackingIssueDto>(total, dtos);
        }

        public override async Task<TrackingIssueDto> CreateAsync(CreateUpdateTrackingIssueDto input)
        {
            var q = await Repository.GetQueryableAsync();

            var maxOrder = await q
                .Where(x => x.ProjectId == input.ProjectId && x.Status == input.Status)
                .Select(x => (int?)x.Order)
                .MaxAsync() ?? 0;

            var e = ObjectMapper.Map<CreateUpdateTrackingIssueDto, TrackingIssue>(input);
            e.Order = maxOrder + 10;

            var now = DateTime.UtcNow;
            if (e.Status == IssueStatus.InProgress && e.StartedTime == null)
                e.StartedTime = now;
            if (e.Status == IssueStatus.Done)
            {
                e.StartedTime ??= now;
                e.DoneTime ??= now;
            }

            e = await Repository.InsertAsync(e, autoSave: true);
            return ObjectMapper.Map<TrackingIssue, TrackingIssueDto>(e);
        }

        public override async Task<TrackingIssueDto> UpdateAsync(Guid id, CreateUpdateTrackingIssueDto input)
        {
            var e = await Repository.GetAsync(id);
            var oldStatus = e.Status;

            ObjectMapper.Map(input, e);

            if (oldStatus != e.Status)
            {
                var now = DateTime.UtcNow;

                if (e.Status == IssueStatus.InProgress && e.StartedTime == null)
                    e.StartedTime = now;

                if (e.Status == IssueStatus.Done)
                {
                    e.StartedTime ??= now;
                    e.DoneTime ??= now;
                }
            }

            e = await Repository.UpdateAsync(e, autoSave: true);
            return ObjectMapper.Map<TrackingIssue, TrackingIssueDto>(e);
        }

        public async Task<TrackingIssueDto> ChangeStatusAsync(Guid id, IssueStatus newStatus)
        {
            var e = await Repository.GetAsync(id);
            if (e.Status == newStatus)
                return ObjectMapper.Map<TrackingIssue, TrackingIssueDto>(e);

            var q = await Repository.GetQueryableAsync();
            var maxOrder = await q
                .Where(x => x.ProjectId == e.ProjectId && x.Status == newStatus)
                .Select(x => (int?)x.Order)
                .MaxAsync() ?? 0;

            e.Status = newStatus;
            e.Order = maxOrder + 10;

            var now = DateTime.UtcNow;
            if (newStatus == IssueStatus.InProgress && e.StartedTime == null)
                e.StartedTime = now;

            if (newStatus == IssueStatus.Done)
            {
                e.StartedTime ??= now;
                e.DoneTime ??= now;
            }

            e = await Repository.UpdateAsync(e, autoSave: true);
            return ObjectMapper.Map<TrackingIssue, TrackingIssueDto>(e);
        }

        public async Task ReorderAsync(IssueReorderInput input)
        {
            var q = await Repository.GetQueryableAsync();

            var items = await q.Where(x => x.ProjectId == input.ProjectId && x.Status == input.Status)
                               .ToDictionaryAsync(x => x.Id, x => x);

            var order = 10;
            foreach (var id in input.OrderedIds)
            {
                if (items.TryGetValue(id, out var e))
                {
                    e.Order = order;
                    order += 10;
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task AssignAsync(Guid id, Guid userId)
        {
            var e = await Repository.GetAsync(id);
            e.AssigneeUserId = userId;
            await Repository.UpdateAsync(e, autoSave: true);
        }

        public async Task UnassignAsync(Guid id)
        {
            var e = await Repository.GetAsync(id);
            e.AssigneeUserId = null;
            await Repository.UpdateAsync(e, autoSave: true);
        }
        
        // KANBAN + GANTT ENTEGRASYONU
        public async Task<List<TrackingIssueDto>> GetListAsync()
        {
            var issues = await Repository.GetListAsync();
            return ObjectMapper.Map<List<TrackingIssue>, List<TrackingIssueDto>>(issues);
        }
        
        public async Task UpdateDatesAsync(Guid id, DateTime start, DateTime end)
        {
            var issue = await Repository.GetAsync(id);
            issue.StartDate = start;
            issue.DueDate = end; // EndDate yerine DueDate kullan
            issue.EstimatedDays = (int)(end - start).TotalDays;
            await Repository.UpdateAsync(issue, autoSave: true);
        }
        
        public async Task UpdateStatusAsync(Guid id, int status)
        {
            var issue = await Repository.GetAsync(id);
            issue.Status = (IssueStatus)status;
            await Repository.UpdateAsync(issue, autoSave: true);
        }
    }
}
