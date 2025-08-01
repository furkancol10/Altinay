using Altinay.Personel.Departments;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

public class DepartmentDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Department, Guid> _departmentRepository;
    private readonly IGuidGenerator _guidGenerator;

    public DepartmentDataSeedContributor(
        IRepository<Department, Guid> departmentRepository,
        IGuidGenerator guidGenerator)
    {
        _departmentRepository = departmentRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _departmentRepository.GetCountAsync() <= 0)
        {
            await _departmentRepository.InsertAsync(new Department(_guidGenerator.Create(), "Yazılım"));
            await _departmentRepository.InsertAsync(new Department(_guidGenerator.Create(), "İnsan Kaynakları"));
            await _departmentRepository.InsertAsync(new Department(_guidGenerator.Create(), "Üretim"));
        }
    }
}
