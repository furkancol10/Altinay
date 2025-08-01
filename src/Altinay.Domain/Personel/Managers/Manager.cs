using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Personel.Managers
{
    public class Manager : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public Manager() { }
        public Manager(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}
