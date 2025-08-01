using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Altinay.Personel
{
    public class LookupDto: EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
