using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Altinay.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Altinay.Projects
{
    [Authorize(AltinayPermissions.Projects.Default)]
    public class ProjectAppService : ApplicationService, IProjectAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectManager _projectManager;

        public ProjectAppService(
            IProjectRepository projectRepository,
            ProjectManager projectManager)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
        }
        public async Task<ProjectDto> GetAsync(string projectCode)
        {
            var project = await _projectRepository.FindByProjectCodeAsync(projectCode);
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }


        public async Task<ProjectDto> CreateAsync(CreateUpdateProjectDto input)
        {
            var project = await _projectManager.CreateAsync(
                input.ProjectCode,
                input.ProjectName,
                input.ProjectDescription
            );

            await _projectRepository.InsertAsync(project);
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

     

        public async Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectListDto input)
        {
            var items = await _projectRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _projectRepository.CountAsync()
                : await _projectRepository.CountAsync(p => p.ProjectCode.Contains(input.Filter));

            return new PagedResultDto<ProjectDto>(
                totalCount,
                ObjectMapper.Map<List<Project>, List<ProjectDto>>(items)
            );
        }
        //AltinayPermissions.Projects.Create

        [Authorize(AltinayPermissions.Projects.Update)]
        public async Task UpdateAsync(string projectCode, CreateUpdateProjectDto input)
        {
            var project = await _projectRepository.FindByProjectCodeAsync(projectCode);
         
            project.ProjectName = input.ProjectName;
            project.ProjectDescription = input.ProjectDescription;

            await _projectRepository.UpdateAsync(project);
        }

        [Authorize(AltinayPermissions.Projects.Delete)]
        public async Task DeleteAsync(string projectCode)
        {
            var project = await _projectRepository.FindByProjectCodeAsync(projectCode);
            if (project != null)
            {
                await _projectRepository.DeleteAsync(project);
            }
        }
    }
}
