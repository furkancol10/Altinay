using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Altinay.EntityFrameworkCore;

namespace Altinay.Meeting
{
    public class EfCoreMeetingRepository
        : EfCoreRepository<AltinayDbContext, Floor, Guid>, IMeetingRepository
    {
        public EfCoreMeetingRepository(IDbContextProvider<AltinayDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
