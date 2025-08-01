using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinay.Personel
{
    public interface IManagerAppService
    {
        Task<List<LookupDto>> GetListAsync();
    }
}
