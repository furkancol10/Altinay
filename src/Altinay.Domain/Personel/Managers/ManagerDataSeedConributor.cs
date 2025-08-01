using Altinay.Personel.Managers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

public class ManagerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Manager, Guid> _managerRepository;
    private readonly IGuidGenerator _guidGenerator;
    
    public ManagerDataSeedContributor(
    IRepository<Manager, Guid> managerRepository,
    IGuidGenerator guidGenerator)
    {
        _managerRepository = managerRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _managerRepository.GetCountAsync() <= 0)
        {
            await _managerRepository.InsertAsync(new Manager(_guidGenerator.Create(), "Ahmet Yılmaz"));
            await _managerRepository.InsertAsync(new Manager(_guidGenerator.Create(), "Ayşe Demir"));
            await _managerRepository.InsertAsync(new Manager(_guidGenerator.Create(), "Fatma Ilgaz"));
        }
    }
}
