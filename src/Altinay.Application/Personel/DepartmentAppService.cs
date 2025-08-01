using Altinay.Personel.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Personel
{
    public class DepartmentAppService:ApplicationService, IDepartmentAppService
    {
        private readonly IRepository<Department, Guid> _departmentRepository;

        public DepartmentAppService(IRepository<Department, Guid> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<LookupDto>> GetListAsync()
        {
            var departments = await _departmentRepository.GetListAsync();
            return departments.Select(d => new LookupDto
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

        }
    }
}
