using System;

namespace Altinay.Blazor.Components.Pages.ProjectTracking
{
    /// <summary>
    /// Gantt Chart veri modeli
    /// </summary>
    public class GanttTaskData
    {
        public string Id { get; set; } = string.Empty;
        public string Task { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int Progress { get; set; }
        public string? ParentId { get; set; }
        public string? Notes { get; set; }
        public string? Dependencies { get; set; }
        public string? Assignee { get; set; }
        public string? Department { get; set; }
        public string? Priority { get; set; }
        public decimal? Budget { get; set; }
        public decimal? ActualCost { get; set; }
        public string? Status { get; set; }
        public string? Risks { get; set; }
        public string? Category { get; set; }
        public string? Location { get; set; }
        public string? TechStack { get; set; }
        public string? Initiative { get; set; }
        public int? EstimatedStoryPoints { get; set; }
        public string? PriorityLevel { get; set; }
    }
}
