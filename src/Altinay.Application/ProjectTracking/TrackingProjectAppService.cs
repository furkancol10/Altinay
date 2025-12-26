using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Authorization;

using Altinay.Domain.ProjectTracking;            // TrackingProject, TrackingProjectMember
using Altinay.ProjectTracking.IAppServices;      // ITrackingProjectAppService
using Altinay.ProjectTracking.ProjectTrackingDtos;
using Altinay.ProjectTracking.CreateUpdateDtos;
using Altinay.Permissions;

namespace Altinay.ProjectTracking
{
    public class TrackingProjectAppService :
        CrudAppService<
            TrackingProject,                         // Entity
            TrackingProjectDto,                      // Read DTO
            Guid,                                    // Id
            PagedAndSortedResultRequestDto,          // List input
            CreateUpdateTrackingProjectDto>,         // Create/Update input
        ITrackingProjectAppService
    {
        private readonly IRepository<TrackingProjectMember, Guid> _memberRepo;
        private readonly IIdentityUserRepository _userRepo;

        public TrackingProjectAppService(
            IRepository<TrackingProject, Guid> projectRepo,
            IRepository<TrackingProjectMember, Guid> memberRepo,
            IIdentityUserRepository userRepo
        ) : base(projectRepo)
        {
            _memberRepo = memberRepo;
            _userRepo = userRepo;
        }

        // Projeye atanmış üyeler (Lookup)
        public async Task<PagedResultDto<LookupDto<Guid>>> GetMembersAsync(Guid projectId)
        {
            var mq = await _memberRepo.GetQueryableAsync();

            var userIds = await mq
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();

            if (userIds.Count == 0)
                return new PagedResultDto<LookupDto<Guid>>(0, new List<LookupDto<Guid>>());

            var users = await _userRepo.GetListAsync();

            var items = users
                .Where(u => userIds.Contains(u.Id))
                .Select(u => new LookupDto<Guid>
                {
                    Id = u.Id,
                    DisplayName = u.UserName // istersen Name + Surname yap
                })
                .ToList();

            return new PagedResultDto<LookupDto<Guid>>(items.Count, items);
        }

        // Tüm site kullanıcıları (üyeleri seçtirmek için)
        public async Task<PagedResultDto<LookupDto<Guid>>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetListAsync();

            var items = users
                .Select(u => new LookupDto<Guid>
                {
                    Id = u.Id,
                    DisplayName = u.UserName
                })
                .ToList();

            return new PagedResultDto<LookupDto<Guid>>(items.Count, items);
        }

        // Proje üyelerini set et (ekle/çıkar mantığı)
        public async Task SetMembersAsync(Guid projectId, List<Guid> userIds)
        {
            var q = await _memberRepo.GetQueryableAsync();
            var existing = await q.Where(x => x.ProjectId == projectId).ToListAsync();

            // eklenecekler (gönderilen listede olup mevcutta olmayanlar)
            var toAdd = userIds.Except(existing.Select(x => x.UserId)).ToList();
            foreach (var uid in toAdd)
            {
                // önemli: ctor kullan; property setter'ları private ise erişim hatası çıkmaz
                await _memberRepo.InsertAsync(new TrackingProjectMember(Guid.NewGuid(), projectId, uid), autoSave: true);
            }

            // silinecekler (mevcutta olup gönderilen listede olmayanlar)
            var toRemove = existing.Where(x => !userIds.Contains(x.UserId)).ToList();
            foreach (var m in toRemove)
            {
                await _memberRepo.DeleteAsync(m, autoSave: true);
            }
        }
    }
}
