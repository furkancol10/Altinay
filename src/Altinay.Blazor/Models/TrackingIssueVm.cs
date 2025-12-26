using System;

namespace Altinay.Blazor.Models;

public enum IssueStatus { ToDo, InProgress, InReview, Done }

public class TrackingIssueVm
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public IssueStatus Status { get; set; } = IssueStatus.ToDo;

    // Gantt ile senkron
    public DateTime Start { get; set; } = DateTime.Today;
    public DateTime End { get; set; } = DateTime.Today.AddDays(1);

    // Görsel / ek alanlar
    public string? AssigneeName { get; set; }
    public int ProgressPercent { get; set; } // opsiyonel
    public string? Description { get; set; }
    public string? Priority { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid? ProjectId { get; set; }
}
