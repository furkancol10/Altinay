using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altinay.Projects;
using Altinay.Files;
using Altinay.ProjectGroups;
using Altinay.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using Autofac.Core;

namespace Altinay.Blazor.Components.Pages.ProjectGroups
{
    public partial class ProjectGroup: IDisposable
    {
        private bool _disposed;
        private IReadOnlyList<ProjectGroupDto> ProjectGroupList { get; set; } = Array.Empty<ProjectGroupDto>();
        private IReadOnlyList<ProjectDto> ProjectList { get; set; } = Array.Empty<ProjectDto>();
        private IReadOnlyList<FileDto> FileList { get; set; } = Array.Empty<FileDto>();

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string? CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private bool CanCreateProjectGroup { get; set; }
        private bool CanEditProjectGroup { get; set; }
        private bool CanDeleteProjectGroup { get; set; }

        private CreateUpdateProjectGroupDto NewProjectGroup { get; set; } = new();
        private Guid EditingProjectGroupId { get; set; }
        private CreateUpdateProjectGroupDto EditingProjectGroup { get; set; } = new();

        // For multi-select create (choose one or more aliases)
        private List<Guid> SelectedFileAliasIds { get; set; } = new();

        private Modal? CreateProjectGroupModal { get; set; }
        private Modal? EditProjectGroupModal { get; set; }

        private Validations? CreateValidationsRef;
        private Validations? EditValidationsRef;

        public void Dispose()
        {
            _disposed = true;
        }

        private async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();

            // Load lists first so we can set valid defaults
            await Task.WhenAll(
                LoadProjectsAsync(),
                LoadFilesAsync(),
                GetProjectGroupsAsync()
            );
        }


        private async Task SetPermissionsAsync()
        {
            CanCreateProjectGroup = await AuthorizationService.IsGrantedAsync(AltinayPermissions.ProjectGroups.Create);
            CanEditProjectGroup = await AuthorizationService.IsGrantedAsync(AltinayPermissions.ProjectGroups.Update);
            CanDeleteProjectGroup = await AuthorizationService.IsGrantedAsync(AltinayPermissions.ProjectGroups.Delete);
        }

        private async Task LoadProjectsAsync()
        {
            var result = await ProjectAppService.GetListAsync(new GetProjectListDto { MaxResultCount = 1000 });
            ProjectList = result.Items;
        }

        private async Task LoadFilesAsync()
        {
            var result = await FileAppService.GetListAsync(new GetFileListDto { MaxResultCount = 1000 });
            FileList = result.Items;
        }

        private async Task GetProjectGroupsAsync()
        {
            var result = await ProjectGroupAppService.GetListAsync(
                new GetProjectGroupListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );
            if (_disposed) return; // Prevent state update after disposal

            ProjectGroupList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProjectGroupDto> e)
        {
            var sorts = e.Columns?
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .ToArray() ?? Array.Empty<string>();

            CurrentSorting = sorts.Length > 0 ? string.Join(",", sorts) : null;
            CurrentPage = e.Page - 1;

            await GetProjectGroupsAsync();
            if (!_disposed)
                await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateProjectGroupModal()
        {
            CreateValidationsRef?.ClearAll();
            NewProjectGroup = new CreateUpdateProjectGroupDto();

            // Preselect valid IDs to avoid null/empty Guid issues
            if (ProjectList?.Any() == true)
                NewProjectGroup.ProjectId = ProjectList[0].Id;

            if (FileList?.Any() == true)
                NewProjectGroup.FileAliasId = FileList[0].Id;

            CreateProjectGroupModal?.Show();
        }

        private void CloseCreateProjectGroupModal()
        {
            CreateProjectGroupModal?.Hide();
        }

        private void OpenEditProjectGroupModal(ProjectGroupDto projectGroup)
        {
            EditValidationsRef?.ClearAll();
            EditingProjectGroupId = projectGroup.Id;
            EditingProjectGroup = ObjectMapper.Map<ProjectGroupDto, CreateUpdateProjectGroupDto>(projectGroup);
            EditProjectGroupModal?.Show();
        }

        private async Task DeleteProjectGroupAsync(ProjectGroupDto projectGroup)
        {
            try
            {
                var confirmMessage = L["Are you sure to delete this record", projectGroup.Id.ToString()];
                if (!await Message.Confirm(confirmMessage))
                {
                    return;
                }

                await ProjectGroupAppService.DeleteAsync(projectGroup.Id);
                await GetProjectGroupsAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void CloseEditProjectGroupModal()
        {
            EditProjectGroupModal?.Hide();
        }

        private async Task CreateProjectGroupAsync()
        {
            try
            {
                if (CreateValidationsRef != null && await CreateValidationsRef.ValidateAll())
                {
                    // If multi-select is used, create one group per selected alias
                    if (SelectedFileAliasIds.Count > 0)
                    {
                        foreach (var aliasId in SelectedFileAliasIds.Distinct())
                        {
                            var dto = new CreateUpdateProjectGroupDto
                            {
                                ProjectId = NewProjectGroup.ProjectId,
                                FileAliasId = aliasId
                            };
                            await ProjectGroupAppService.CreateAsync(dto);
                        }
                    }
                    else
                    {
                        await ProjectGroupAppService.CreateAsync(NewProjectGroup);
                    }

                    await GetProjectGroupsAsync();
                    if (CreateProjectGroupModal != null)
                        await CreateProjectGroupModal.Hide();
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private Task OnSelectedFileAliasesChanged(IReadOnlyList<Guid> values)
        {
            SelectedFileAliasIds = values?.ToList() ?? new List<Guid>();
            return Task.CompletedTask;
        }
        private async Task UpdateProjectGroupAsync()
        {
            try
            {
                if (EditValidationsRef != null && await EditValidationsRef.ValidateAll())
                {
                    await ProjectGroupAppService.UpdateAsync(EditingProjectGroupId, EditingProjectGroup);
                    await GetProjectGroupsAsync();
                    if (EditProjectGroupModal != null)
                        await EditProjectGroupModal.Hide();
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}