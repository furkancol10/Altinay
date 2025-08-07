using System;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Personel.Departments
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
    }
}