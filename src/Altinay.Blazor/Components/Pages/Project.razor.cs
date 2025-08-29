using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altinay.Projects;
using Altinay.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Altinay.Blazor.Components.Pages.Projects;

public partial class Project
{
    private IReadOnlyList<ProjectDto> ProjectList { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateProject { get; set; }
    private bool CanEditProject { get; set; }
    private bool CanDeleteProject { get; set; }

    private CreateUpdateProjectDto NewProject { get; set; } = new();
    private Guid EditingProjectId { get; set; }
    private CreateUpdateProjectDto EditingProject { get; set; } = new();

    private Modal CreateProjectModal { get; set; }
    private Modal EditProjectModal { get; set; }

    private Validations CreateValidationsRef;
    private Validations EditValidationsRef;

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetProjectsAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateProject = await AuthorizationService
            .IsGrantedAsync(AltinayPermissions.Projects.Create);

        CanEditProject = await AuthorizationService
            .IsGrantedAsync(AltinayPermissions.Projects.Update);

        CanDeleteProject = await AuthorizationService
            .IsGrantedAsync(AltinayPermissions.Projects.Delete);
    }

    private async Task GetProjectsAsync()
    {
        var result = await ProjectAppService.GetListAsync(
            new GetProjectListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        ProjectList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProjectDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .DefaultIfEmpty()
            .Aggregate((a, b) => string.Join(",", new[] { a, b }));
        CurrentPage = e.Page - 1;

        await GetProjectsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateProjectModal()
    {
        CreateValidationsRef.ClearAll();
        NewProject = new CreateUpdateProjectDto();
        CreateProjectModal.Show();
    }

    private void CloseCreateProjectModal()
    {
        CreateProjectModal.Hide();
    }

    private void OpenEditProjectModal(ProjectDto project)
    {
        EditValidationsRef.ClearAll();
        EditingProjectId = project.Id;
        EditingProject = ObjectMapper.Map<ProjectDto, CreateUpdateProjectDto>(project);
        EditProjectModal.Show();
    }

    private async Task DeleteProjectAsync(ProjectDto project)
    {
        try
        {
            var confirmMessage = L["Are you sure to delete this record", project.ProjectName];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await ProjectAppService.DeleteAsync(project.ProjectCode);
            await GetProjectsAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void CloseEditProjectModal()
    {
        EditProjectModal.Hide();
    }

    private async Task CreateProjectAsync()
    {
        try
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await ProjectAppService.CreateAsync(NewProject);
                await GetProjectsAsync();
                CreateProjectModal.Hide();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task UpdateProjectAsync()
    {
        try
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await ProjectAppService.UpdateAsync(EditingProject.ProjectCode, EditingProject);
                await GetProjectsAsync();
                EditProjectModal.Hide();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
}
