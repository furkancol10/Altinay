using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Altinay.Personel;
using Altinay.EntityFrameworkCore;

namespace Altinay.PersonelRequests
{
    public class EfCorePersonelRequestRepository
        : EfCoreRepository<AltinayDbContext, PersonelRequest, Guid>, IPersonelRequestRepository
    {
        public EfCorePersonelRequestRepository(IDbContextProvider<AltinayDbContext> dbContextProvider): base(dbContextProvider)
        {
        }

    }
}
