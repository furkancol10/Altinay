using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Personel
{
    public interface IPersonelRequestRepository : IRepository<PersonelRequest>
    {
        
    }
}