using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Altinay.Personel
{
    public interface IPersonelRequestAppService :
        ICrudAppService<
            PersonelRequestDto,
            Guid,
            PersonelRequestInputListDto,
            CreateUpdatePersonelRequestDto
            >
    {
        //Specific methods are here
    }
}
