using System;
using Volo.Abp.Application.Dtos;

namespace Altinay.Personel
{
    public class ManagerDto : EntityDto<Guid>
    {
        public string Name { get; private set; }
    }
}
