using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Altinay.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altinay.Blazor.Components.Gantt;

public class GanttChartBase : ComponentBase, IAsyncDisposable
{
    [Inject] protected IJSRuntime JS { get; set; } = default!;
    [Parameter] public List<TrackingIssueVm> Items { get; set; } = new();
    [Parameter] public DateTime MinDate { get; set; } = DateTime.Today.AddDays(-7);
    [Parameter] public DateTime MaxDate { get; set; } = DateTime.Today.AddDays(30);
    [Parameter] public int PxPerDay { get; set; } = 24;
    [Parameter] public EventCallback<(Guid id, DateTime start, DateTime end)> OnTaskDatesChanged { get; set; }

    protected int RowHeight { get; set; } = 40;
    protected int BarTopOffset { get; set; } = 8;
    protected string PxPerDayPx => $"{PxPerDay}px";
    protected ElementReference _ganttBodyRef;
    protected DotNetObjectReference<GanttChartBase>? _selfRef;

    protected IEnumerable<DateTime> HeaderDays =>
        Enumerable.Range(0, (int)(MaxDate.Date - MinDate.Date).TotalDays + 1)
            .Select(offset => MinDate.Date.AddDays(offset));

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _selfRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("AltinayGantt.init", _ganttBodyRef, _selfRef, new {
                pxPerDay = PxPerDay,
                minDateTicks = MinDate.Date.Ticks,
                rowHeight = RowHeight,
                barTopOffset = BarTopOffset
            });
        }
    }

    [JSInvokable]
    public async Task OnJsDragFinished(string idStr, double deltaDays)
    {
        var id = Guid.Parse(idStr);
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item is null) return;

        var delta = (int)Math.Round(deltaDays);
        var dur = (item.End.Date - item.Start.Date).Days;
        item.Start = item.Start.Date.AddDays(delta);
        item.End = item.Start.AddDays(dur);

        StateHasChanged();
        if (OnTaskDatesChanged.HasDelegate)
            await OnTaskDatesChanged.InvokeAsync((item.Id, item.Start, item.End));
    }

    [JSInvokable]
    public async Task OnJsResizeFinished(string idStr, string edge, double deltaDays)
    {
        var id = Guid.Parse(idStr);
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item is null) return;

        var delta = (int)Math.Round(deltaDays);
        if (edge == "left") item.Start = item.Start.Date.AddDays(delta);
        if (edge == "right") item.End = item.End.Date.AddDays(delta);
        if (item.Start > item.End) item.Start = item.End;

        StateHasChanged();
        if (OnTaskDatesChanged.HasDelegate)
            await OnTaskDatesChanged.InvokeAsync((item.Id, item.Start, item.End));
    }

    public async ValueTask DisposeAsync()
    {
        try { await JS.InvokeVoidAsync("AltinayGantt.dispose", _ganttBodyRef); } catch { }
        _selfRef?.Dispose();
    }
}
