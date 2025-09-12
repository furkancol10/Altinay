using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Altinay.Domain.ProjectTracking;

public class ProjectTrackingDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<TrackingProject, Guid> _projectRepository;
    private readonly IRepository<TrackingIssue, Guid> _issueRepository;
    private readonly IGuidGenerator _guidGenerator;

    public ProjectTrackingDataSeedContributor(
        IRepository<TrackingProject, Guid> projectRepository,
        IRepository<TrackingIssue, Guid> issueRepository,
        IGuidGenerator guidGenerator)
    {
        _projectRepository = projectRepository;
        _issueRepository = issueRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        // Proje var mı kontrol et
        var projectCount = await _projectRepository.GetCountAsync();
        if (projectCount <= 0)
        {
            var project = new TrackingProject
            {
                Name = "Altınay Portal",
                Key = "ALT",
                Description = "Demo project for Project Tracking"
            };

            await _projectRepository.InsertAsync(project, autoSave: true);

            // Örnek kartlar
            await _issueRepository.InsertAsync(new TrackingIssue
            {
                ProjectId = project.Id,
                Title = "Temizlik",
                Status = IssueStatus.Todo,
                Priority = IssuePriority.Medium,
                DueDate = DateTime.UtcNow.AddDays(3)
            });

            await _issueRepository.InsertAsync(new TrackingIssue
            {
                ProjectId = project.Id,
                Title = "Login bug fix",
                Status = IssueStatus.InProgress,
                Priority = IssuePriority.High,
                StartedTime = DateTime.UtcNow.AddDays(-1)
            });

            await _issueRepository.InsertAsync(new TrackingIssue
            {
                ProjectId = project.Id,
                Title = "Rapor grafikleri",
                Status = IssueStatus.Review,
                Priority = IssuePriority.Low,
                DueDate = DateTime.UtcNow.AddDays(7)
            });

            await _issueRepository.InsertAsync(new TrackingIssue
            {
                ProjectId = project.Id,
                Title = "Deploy pipeline",
                Status = IssueStatus.Done,
                Priority = IssuePriority.Critical,
                StartedTime = DateTime.UtcNow.AddDays(-3),
                DoneTime = DateTime.UtcNow.AddDays(-1)
            });

            await _issueRepository.InsertAsync(new TrackingIssue
            {
                ProjectId = project.Id,
                Title = "Form validasyonları",
                Status = IssueStatus.Todo,
                Priority = IssuePriority.High,
                DueDate = DateTime.UtcNow.AddDays(-2) // overdue
            });
        }
    }
}
