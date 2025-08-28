using Altinay.Files;
using Altinay.Permissions;
using Altinay.ProjectGroups;
using Altinay.Projects;
using Autofac.Core;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace Altinay.Blazor.Components.Pages.ProjectGroups
{
    public partial class ProjectGroup : IDisposable
    {
        // modal refs
        private Modal AddPersonModal;

        // state
        private Guid CurrentGroupId;
        private Guid SelectedUserId;
        private List<IdentityUserDto> AvailableUsers = new();

        [Inject] private IIdentityUserAppService IdentityUserAppService { get; set; } = default!;

        private IReadOnlyList<ProjectGroupDto> ProjectGroupList { get; set; }
        private IReadOnlyList<ProjectDto> ProjectList { get; set; }
        private IReadOnlyList<FileDto> FileList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private void OnFileAliasChanged(Guid value) => NewProjectGroup.FileAliasId = value;

        private bool CanCreateProjectGroup { get; set; }
        private bool CanEditProjectGroup { get; set; }
        private bool CanDeleteProjectGroup { get; set; }

        private CreateUpdateProjectGroupDto NewProjectGroup { get; set; } = new();
        private Guid EditingProjectGroupId { get; set; }
        private CreateUpdateProjectGroupDto EditingProjectGroup { get; set; } = new();

        // For multi-select create (choose one or more aliases)

        private Modal CreateProjectGroupModal { get; set; }
        private Modal EditProjectGroupModal { get; set; }

        private Validations CreateValidationsRef;
        private Validations EditValidationsRef;


        // In ProjectGroup.razor.cs

        private async Task OpenAddPersonModal(ProjectGroupDto group)
        {
            CurrentGroupId = group.Id;

            var result = await IdentityUserAppService.GetListAsync(new GetIdentityUsersInput());
            AvailableUsers = result.Items
            .Where(u => group.Users.All(g => g.Id != u.Id))
              .ToList();

            SelectedUserId = Guid.Empty;
            await AddPersonModal.Show();
        }

        private Task CloseAddPersonModal()
        {
            return AddPersonModal.Hide();
        }

        private async Task AddPersonToGroupAsync()
        {
            if (SelectedUserId == Guid.Empty)
            {
                await Message.Error("Please select a user to add.");
                return;
            }

            try
            {
                // Call your AppService to add the user to the group
                await ProjectGroupAppService.AddUserToGroupAsync(CurrentGroupId, SelectedUserId);

                // Optionally refresh the group list or the current group
                await GetProjectGroupsAsync();

                await AddPersonModal.Hide();
                await Message.Success("User added to group successfully.");
            }
            catch (Exception ex)
            {
                await Message.Error($"Failed to add user: {ex.Message}");
            }
        }
        
        private void OnFileAliasCheckboxChanged(ChangeEventArgs e, Guid fileId)
        {
            var isChecked = e.Value == null ? false : (bool)e.Value == true;
            // For Blazor, e.Value is usually "on" for checked, null for unchecked
            if ((isChecked == true) && (NewProjectGroup.FileAliasIds.Contains(fileId) == false))
            {
                NewProjectGroup.FileAliasIds.Add(fileId);
            }
            else if ((isChecked == false) && (NewProjectGroup.FileAliasIds.Contains(fileId)))
            {
                NewProjectGroup.FileAliasIds.Remove(fileId);
            }
        }
  
       protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();

            // Load lists first so we can set valid defaults
            await Task.WhenAll(
                LoadProjectsAsync(),
                LoadFilesAsync(),
                GetProjectGroupsAsync()
            );

            await base.OnInitializedAsync();
        }


        private async Task SetPermissionsAsync()
        {
            CanCreateProjectGroup = await AuthorizationService.IsGrantedAsync(AltinayPermissions.ProjectGroups.Create);
            CanEditProjectGroup = await AuthorizationService.IsGrantedAsync(AltinayPermissions.ProjectGroups.Update);
            CanDeleteProjectGroup = await AuthorizationService.IsGrantedAsync(AltinayPermissions.ProjectGroups.Delete);
        }

        private async Task LoadProjectsAsync()
        {
            try
            {
                var result = await ProjectAppService.GetListAsync(new GetProjectListDto { MaxResultCount = 1000 });
                ProjectList = result.Items;
            }
            catch (Volo.Abp.Authorization.AbpAuthorizationException)
            {
                // Handle lack of permission gracefully
                await Message.Error("You do not have permission to view projects.");
                ProjectList = Array.Empty<ProjectDto>();
            }
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
            //if (_disposed) return; // Prevent state update after disposal

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
            //if (!_disposed)
            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateProjectGroupModal()
        {
            CreateValidationsRef.ClearAll();
            NewProjectGroup = new CreateUpdateProjectGroupDto();

            // Preselect valid IDs to avoid null/empty Guid issues
            //if (ProjectList?.Any() == true)
                //NewProjectGroup.ProjectId = ProjectList[0].Id;

            if (FileList?.Any() == true)
                NewProjectGroup.FileAliasId = FileList[0].Id;

            CreateProjectGroupModal.Show();
        }

        private void CloseCreateProjectGroupModal()
        {
            CreateProjectGroupModal.Hide();
        }

        private void OpenEditProjectGroupModal(ProjectGroupDto projectGroup)
        {
            EditValidationsRef.ClearAll();
            EditingProjectGroupId = projectGroup.Id;
            EditingProjectGroup = ObjectMapper.Map<ProjectGroupDto, CreateUpdateProjectGroupDto>(projectGroup);
            EditProjectGroupModal.Show();
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
            EditProjectGroupModal.Hide();
        }

        private async Task CreateProjectGroupAsync()
        {
            try
            {
                if (await CreateValidationsRef.ValidateAll())
                {
                    // Get all existing project group combinations for the selected project and alias
                    var existingGroups = ProjectGroupList
                        .Select(pg => new { pg.ProjectId, pg.FileAliasId })
                        .ToHashSet();

                    // Find which selected (project, alias) pairs already exist
                    var duplicatePairs = NewProjectGroup.FileAliasIds
                        .Where(aliasId => existingGroups.Contains(new { ProjectId = NewProjectGroup.ProjectId, FileAliasId = aliasId }))
                        .ToList();

                   
                       //message:youhave to choose a project
                    if (NewProjectGroup.ProjectId == Guid.Empty)
                    {
                        await Message.Error("You have to choose a project before creating a project group.");
                        return;
                    }


                    

                    if (duplicatePairs.Any())
                    {
                        var duplicateNames = FileList
                            .Where(f => duplicatePairs.Contains(f.Id))
                            .Select(f => f.FileAlias)
                            .ToList();

                        var message = $"You cannot create an existing group for: {string.Join(", ", duplicateNames)}. Please rechoose.";
                        await Message.Error(message);
                        return;
                    }

                    // Create new groups for non-duplicate (project, alias) pairs
                    foreach (var aliasId in NewProjectGroup.FileAliasIds.Distinct())
                    {
                        var dto = new CreateUpdateProjectGroupDto
                        {
                            ProjectId = NewProjectGroup.ProjectId,
                            FileAliasId = aliasId
                        };
                        await ProjectGroupAppService.CreateAsync(dto);
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