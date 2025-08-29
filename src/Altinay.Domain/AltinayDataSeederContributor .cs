using System;
using System.Threading.Tasks;
using Altinay.Projects;
using Altinay.ProjectGroups;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Altinay
{
    public class AltinayDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ProjectManager _projectManager;
        private readonly IRepository<ProjectGroup, Guid> _projectGroupRepository;
        private readonly ProjectGroupManager _projectGroupManager;

        public AltinayDataSeederContributor(
            IRepository<Project, Guid> projectRepository,
            ProjectManager projectManager,
            IRepository<ProjectGroup, Guid> projectGroupRepository,
            ProjectGroupManager projectGroupManager)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
            _projectGroupRepository = projectGroupRepository;
            _projectGroupManager = projectGroupManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Seed Projects
            if (await _projectRepository.GetCountAsync() == 0)
            {
                var project1 = await _projectManager.CreateAsync("PRJ001", "Project One", null);
                await _projectRepository.InsertAsync(project1, autoSave: true);

                var project2 = await _projectManager.CreateAsync("PRJ002", "Project Two", null);
                await _projectRepository.InsertAsync(project2, autoSave: true);
            }

            // Get seeded projects
            var prj1 = await _projectRepository.FirstOrDefaultAsync(p => p.ProjectCode == "PRJ001");
            var prj2 = await _projectRepository.FirstOrDefaultAsync(p => p.ProjectCode == "PRJ002");

            // Use static Guids for FileAliasId for demonstration
            var fileAliasId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var fileAliasId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // Seed ProjectGroups
          
        }
    }
}
