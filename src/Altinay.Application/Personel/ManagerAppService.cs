using Altinay.Personel.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Personel
{
    public class ManagerAppService:ApplicationService, IManagerAppService
    {
        private readonly IRepository<Manager, Guid> _managerRepository;

        public ManagerAppService(IRepository<Manager, Guid> managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<List<LookupDto>> GetListAsync()
        {
            var items = await _managerRepository.GetListAsync();
            return items.Select(x => new LookupDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
