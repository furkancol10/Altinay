using System;
using System.Threading.Tasks;
using Altinay.Projects;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Altinay
{
    public class AltinayDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ProjectManager _projectManager;

        public AltinayDataSeederContributor(
            IRepository<Project, Guid> projectRepository,
            ProjectManager projectManager)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _projectRepository.GetCountAsync() > 0)
            {
                return; // Data already seeded
            }

            var project1 = await _projectManager.CreateAsync(
                "PRJ001",
                "Project One",
                "Description for Project One"
            );
            await _projectRepository.InsertAsync(project1, autoSave: true);

            var project2 = await _projectManager.CreateAsync(
                "PRJ002",
                "Project Two",
                "Description for Project Two"
            );
            await _projectRepository.InsertAsync(project2, autoSave: true);
        }
    }
}
