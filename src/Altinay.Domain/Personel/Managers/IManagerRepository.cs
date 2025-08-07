using System;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Personel.Managers
{
    public interface IManagerRepository : IRepository<Manager, Guid>
    {
    }
}
