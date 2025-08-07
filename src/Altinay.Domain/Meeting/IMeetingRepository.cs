using System;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Meeting
{
    public interface IMeetingRepository : IRepository<Floor, Guid>
    {
    }
}
