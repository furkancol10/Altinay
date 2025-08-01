using Altinay.EntityFrameworkCore;
using Altinay.Personel.Departments;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Altinay.PersonelRequests
{
    public class DepartmentRepository : EfCoreRepository<AltinayDbContext, Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(IDbContextProvider<AltinayDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
