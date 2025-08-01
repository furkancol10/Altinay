using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinay.Personel
{
    public interface IDepartmentAppService
    {
        Task<List<LookupDto>> GetListAsync();
    }
}
