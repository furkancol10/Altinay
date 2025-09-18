using System;

namespace Altinay.Blazor.Models
{
    public class GanttTaskData
    {
        public string Id { get; set; } = "";
        public string Task { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int Progress { get; set; }
        public string Status { get; set; } = "";
        public string Priority { get; set; } = "";
        public string? Description { get; set; }
        public string? Assignee { get; set; }
        public string? Category { get; set; }
        public string? Initiative { get; set; }
        public string? Dependencies { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
