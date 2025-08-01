using Altinay.EntityFrameworkCore;
using Altinay.Personel.Managers;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Altinay.PersonelRequests
{
    public class ManagerRepository : EfCoreRepository<AltinayDbContext, Manager,Guid>,IManagerRepository
    {
        public ManagerRepository(IDbContextProvider<AltinayDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
