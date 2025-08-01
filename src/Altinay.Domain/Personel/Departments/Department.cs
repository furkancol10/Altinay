using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Personel.Departments
{
    public class Department : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        private Department() { }
        public Department(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}
