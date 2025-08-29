using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;

namespace Altinay.Projects
{
    public class ProjectManager : DomainService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Project> CreateAsync(string projectCode, string projectName, string? projectDescription)
        {
            Check.NotNullOrWhiteSpace(projectCode, nameof(projectCode));

            var existingProject = await _projectRepository.FindByProjectCodeAsync(projectCode);
            if (existingProject != null)
            {
                throw new Exception($"Project with code {projectCode} already exists.");
            }
            return new Project(projectCode, projectName, projectDescription);

        }
        public async Task<Project> ChangeCodeAsync(Project project, string newProjectCode)
        {
            Check.NotNull(project, nameof(project));
            Check.NotNullOrWhiteSpace(newProjectCode, nameof(newProjectCode));
            var existingProject = await _projectRepository.FindByProjectCodeAsync(newProjectCode);
            if (existingProject != null && existingProject.Id != project.Id)
            {
                throw new Exception($"Project with code {newProjectCode} already exists.");
            }
            return project.ChangeCode(newProjectCode);
        }
    }

}