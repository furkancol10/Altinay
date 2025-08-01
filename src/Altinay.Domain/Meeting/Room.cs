using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Altinay.Enums;

namespace Altinay.Meeting
{
    public class Room : FullAuditedAggregateRoot<Guid>
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid FloorID { get;  set; }
        public Floor Floor { get;  set; }
        public int Capacity { get;  set; }
        public Availibility Availibility { get;  set; }


    }
}
