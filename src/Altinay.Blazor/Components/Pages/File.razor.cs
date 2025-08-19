using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altinay.Files;
using Altinay.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Altinay.Blazor.Components.Pages.Files;

public partial class File
{
    private IReadOnlyList<FileDto> FileList { get; set; } = Array.Empty<FileDto>();

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateFile { get; set; }
    private bool CanEditFile { get; set; }
    private bool CanDeleteFile { get; set; }

    private CreateUpdateFileDto NewFile { get; set; } = new();
    private Guid EditingFileId { get; set; }
    private CreateUpdateFileDto EditingFile { get; set; } = new();

    private Modal CreateFileModal { get; set; }
    private Modal EditFileModal { get; set; }

    private Validations CreateValidationsRef;
    private Validations EditValidationsRef;

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetFilesAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateFile = await AuthorizationService
            .IsGrantedAsync(AltinayPermissions.Files.Create);

        CanEditFile = await AuthorizationService
            .IsGrantedAsync(AltinayPermissions.Files.Update);

        CanDeleteFile = await AuthorizationService
            .IsGrantedAsync(AltinayPermissions.Files.Delete);
    }

    private async Task GetFilesAsync()
    {
        var result = await FileAppService.GetListAsync(
            new GetFileListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        FileList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FileDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .DefaultIfEmpty()
            .Aggregate((a, b) => string.Join(",", new[] { a, b }));
        CurrentPage = e.Page - 1;

        await GetFilesAsync();
        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateFileModal()
    {
        CreateValidationsRef.ClearAll();
        NewFile = new CreateUpdateFileDto();
        CreateFileModal.Show();
    }

    private void CloseCreateFileModal()
    {
        CreateFileModal.Hide();
    }

    private void OpenEditFileModal(FileDto file)
    {
        EditValidationsRef.ClearAll();
        EditingFileId = file.Id;
        EditingFile = ObjectMapper.Map<FileDto, CreateUpdateFileDto>(file);
        EditFileModal.Show();
    }

    private async Task DeleteFileAsync(FileDto file)
    {
        try
        {
            var confirmMessage = L["Are you sure to delete this record", file.FileAlias];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            // note: your IFileAppService.DeleteAsync takes string FileAlias
            await FileAppService.DeleteAsync(file.FileAlias);
            await GetFilesAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void CloseEditFileModal()
    {
        EditFileModal.Hide();
    }

    private async Task CreateFileAsync()
    {
        try
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await FileAppService.CreateAsync(NewFile);
                await GetFilesAsync();
                CreateFileModal.Hide();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task UpdateFileAsync()
    {
        try
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await FileAppService.UpdateAsync(EditingFile.FileAlias, EditingFile);
                await GetFilesAsync();
                EditFileModal.Hide();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
}
