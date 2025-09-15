using Altinay.Meeting;
using Altinay.Personel;
using Altinay.Personel.Departments;
using Altinay.Personel.Managers;
using Altinay.Projects;
using Altinay.ProjectGroups;
using Altinay.Files;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Altinay.Domain.ProjectTracking;




namespace Altinay.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class AltinayDbContext :
    AbpDbContext<AltinayDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    //
    //Personel Request Form
    //
    public DbSet<PersonelRequest> PersonelRequests { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Manager> Managers { get; set; }
    //
    //Meeting Room Booking
    //
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Floor> Bookings { get; set; }
    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    //
    //Projects
    //
    public DbSet<Project> Projects { get; set; }
    //
    //Files
    //
    public DbSet<File> Files { get; set; }
    //
    //Project Groups
    public DbSet<ProjectGroup> ProjectGroups { get; set; }
    public DbSet<ProjectGroupUser> ProjectGroupUsers { get; set; } // <-- FIXED: was IdentityUser

    //Project Tracking
    public DbSet<TrackingProject> TrackingProjects { get; set; }
    public DbSet<TrackingIssue> TrackingIssues { get; set; }
    public DbSet<TrackingComment> TrackingComments { get; set; }
    public DbSet<TrackingAttachment> TrackingAttachments { get; set; }
    public DbSet<TrackingTag> TrackingTags { get; set; }
    public DbSet<TrackingIssueTag> TrackingIssueTags { get; set; }
    public DbSet<TrackingTimeLog> TrackingTimeLogs { get; set; }

    //Project Tracking Members
    public DbSet<TrackingProjectMember> TrackingProjectMembers { get; set; }

    //Smart Notifications
    public DbSet<SmartNotification> SmartNotifications { get; set; }
    public DbSet<NotificationSetting> NotificationSettings { get; set; }





    public AltinayDbContext(DbContextOptions<AltinayDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProjectGroup>(entity =>
        {
            entity.Ignore(e => e.ExtraProperties);
        });

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */
                            //
                            /* Meeting Room Booking */
                            //
        builder.Entity<PersonelRequest>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "PersonelRequest", AltinayConsts.DbSchema);
            b.ConfigureByConvention();
            //
            //Temel Alanlar
            //
            b.Property(x => x.JobTitle).IsRequired().HasMaxLength(128);
            b.Property(x => x.DepartmentId).IsRequired();
            b.Property(x => x.ManagerId).IsRequired();
            b.Property(x => x.NumberOfPersonel).IsRequired();
            b.Property(x => x.RequestDate).IsRequired();
            //
            //Talep Türü ve Nedenleri
            //
            b.Property(x => x.RequestType).IsRequired();
            b.Property(x => x.RequestReason).HasMaxLength(512);
            b.Property(x => x.ReasonForNewPosition).HasMaxLength(512);
            b.Property(x => x.ReasonForLeaving).HasMaxLength(512);
            b.Property(x => x.LeavingDate);
            //
            //Yaş Aralığı
            //
            b.Property(x => x.MinAge).IsRequired();
            b.Property(x => x.MaxAge).IsRequired();
            //
            //Personel Bilgileri
            //
            b.Property(x => x.Gender).IsRequired().HasMaxLength(128);
            b.Property(x => x.Location).IsRequired().HasMaxLength(256);
            b.Property(x => x.ExperienceStatus).IsRequired();
            b.Property(x => x.OtherQualifications).IsRequired().HasMaxLength(256);
            //
            //Varsa Ayrılan kişi bilgileri
            //
            b.Property(x => x.ReplacementPersonName).HasMaxLength(128);
            //
            //Talep Eden Bilgileri
            //
            b.Property(x => x.RequesterName).IsRequired().HasMaxLength(128);
            b.Property(x => x.RequesterTitle).IsRequired().HasMaxLength(128);
            
        });

        //TRACKİNG PROJECT
        // TrackingProject
        builder.Entity<TrackingProject>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "TrackingProject", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Key).IsRequired().HasMaxLength(8);

            b.HasIndex(x => x.Key).IsUnique();
        });

        // TrackingIssue
        builder.Entity<TrackingIssue>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "TrackingIssue", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Title).IsRequired().HasMaxLength(128);
            b.Property(x => x.Status).IsRequired();   // enum
            b.Property(x => x.Priority).IsRequired(); // enum

            // indexler
            b.HasIndex(x => x.ProjectId);
            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.DueDate);

            // ilişki: Issue -> Project
            b.HasOne<TrackingProject>()
             .WithMany()
             .HasForeignKey(x => x.ProjectId)
             .OnDelete(DeleteBehavior.Cascade);
        });



        //
        //Yöneticiler
        //
        builder.Entity<Manager>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "Manager", AltinayConsts.DbSchema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });
        //
        //Departmanlar
        //
        builder.Entity<Department>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "Department", AltinayConsts.DbSchema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });        
                            //
                            /* Meeting Room Booking */
                            //
        builder.Entity<Floor>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "Floor", AltinayConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
        });

        builder.Entity<Room>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "Room", AltinayConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            //
            //Which Floor this room belongs to
            //
            b.HasOne(r => r.Floor)
            .WithMany()
            .HasForeignKey(r => r.FloorID);
        });

        builder.Entity<Booking>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "Booking", AltinayConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
                        
            b.HasOne(r => r.Room)
            .WithMany()
            .HasForeignKey(r => r.RoomID)
            .IsRequired();     
        });
        //PROJECTS 
        builder.Entity<Project>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "Project", AltinayConsts.DbSchema);

            b.ConfigureByConvention(); // configure Id and auditing properties automatically
            b.Property(x => x.ProjectCode);

            b.Property(x => x.ProjectName)
                .IsRequired()
                .HasMaxLength(128);

            b.Property(x => x.ProjectDescription);
        });

        
        builder.Entity<File>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "File", AltinayConsts.DbSchema);
            b.ConfigureByConvention(); // configure Id and auditing properties automatically
            b.Property(x => x.FileAlias)
                .IsRequired()
                .HasMaxLength(128);
            b.Property(x => x.FileDescription)
                .HasMaxLength(512);
            b.Property(x => x.IsActive);
        });
        //PROJECT GROUPS
        builder.Entity<ProjectGroup>(b =>
            {
                b.ToTable(AltinayConsts.DbTablePrefix + "ProjectGroup", AltinayConsts.DbSchema);
                b.ConfigureByConvention(); // configure Id and auditing properties automatically

                b.Property(x => x.GroupName)
                    .IsRequired()
                    .HasMaxLength(128);

                b.Property(x => x.ProjectId)
                    .IsRequired();
                b.Property(x => x.FileAliasId)
                    .IsRequired();
                b.Property(x => x.GroupName)
                    .HasMaxLength(128);
            });
        //PROJECT GROUP USERS

        builder.Entity<ProjectGroupUser>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "ProjectGroupUser", AltinayConsts.DbSchema);
            b.ConfigureByConvention(); // configure Id and auditing properties automatically

            b.Property(x => x.ProjectGroupId)
                .IsRequired();
            b.Property(x => x.IdentityUserId)
                .IsRequired();

            // Define composite primary key
            b.HasKey(x => new { x.ProjectGroupId, x.IdentityUserId });

            b.HasOne(x => x.ProjectGroup)
                .WithMany(p => p.Users)
                .HasForeignKey(x => x.ProjectGroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.IdentityUser)
                .WithMany()
                .HasForeignKey(x => x.IdentityUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure a user can only be in a group once
            b.HasIndex(x => new { x.ProjectGroupId, x.IdentityUserId }).IsUnique();
        });

        //TRACKİNG MEMBER 
        builder.Entity<TrackingProjectMember>(b =>
        {
            b.ToTable("AppTrackingProjectMembers");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.ProjectId, x.UserId }).IsUnique();
        });

        //TRACKİNG COMMENT
        builder.Entity<TrackingComment>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "TrackingComment", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Content).IsRequired().HasMaxLength(2000);
            b.Property(x => x.CreationTime).IsRequired();

            // Indexes
            b.HasIndex(x => x.IssueId);
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.CreationTime);

            // Relationships
            b.HasOne<TrackingIssue>()
             .WithMany()
             .HasForeignKey(x => x.IssueId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne<IdentityUser>()
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        //TRACKING ATTACHMENT
        builder.Entity<TrackingAttachment>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "TrackingAttachment", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.FileName).IsRequired().HasMaxLength(255);
            b.Property(x => x.FilePath).IsRequired().HasMaxLength(500);
            b.Property(x => x.ContentType).IsRequired().HasMaxLength(100);

            // Indexes
            b.HasIndex(x => x.IssueId);
            b.HasIndex(x => x.UploadedByUserId);

            // Relationships
            b.HasOne<TrackingIssue>()
             .WithMany()
             .HasForeignKey(x => x.IssueId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne<IdentityUser>()
             .WithMany()
             .HasForeignKey(x => x.UploadedByUserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        //TRACKING TAG
        builder.Entity<TrackingTag>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "TrackingTag", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.Property(x => x.Color).IsRequired().HasMaxLength(20);

            // Indexes
            b.HasIndex(x => x.ProjectId);
            b.HasIndex(x => new { x.Name, x.ProjectId }).IsUnique();

            // Relationships
            b.HasOne<TrackingProject>()
             .WithMany()
             .HasForeignKey(x => x.ProjectId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        //TRACKING ISSUE TAG (Many-to-Many)
        builder.Entity<TrackingIssueTag>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "TrackingIssueTag", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            // Indexes
            b.HasIndex(x => x.IssueId);
            b.HasIndex(x => x.TagId);
            b.HasIndex(x => new { x.IssueId, x.TagId }).IsUnique();

            // Relationships
            b.HasOne<TrackingIssue>()
             .WithMany()
             .HasForeignKey(x => x.IssueId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne<TrackingTag>()
             .WithMany()
             .HasForeignKey(x => x.TagId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        //TRACKING TIME LOG
        builder.Entity<TrackingTimeLog>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "TrackingTimeLog", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Description).HasMaxLength(500);

            // Indexes
            b.HasIndex(x => x.IssueId);
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.StartTime);
            b.HasIndex(x => x.IsActive);

            // Relationships
            b.HasOne<TrackingIssue>()
             .WithMany()
             .HasForeignKey(x => x.IssueId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne<IdentityUser>()
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        //SMART NOTIFICATION
        builder.Entity<SmartNotification>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "SmartNotification", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.NotificationType).IsRequired().HasMaxLength(50);
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.Message).IsRequired().HasMaxLength(1000);

            // Indexes
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.NotificationType);
            b.HasIndex(x => x.IsSent);
            b.HasIndex(x => x.IsRead);
            b.HasIndex(x => x.SentAt);

            // Relationships
            b.HasOne<IdentityUser>()
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        //NOTIFICATION SETTING
        builder.Entity<NotificationSetting>(b =>
        {
            b.ToTable(AltinayConsts.DbTablePrefix + "NotificationSetting", AltinayConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.WorkStartTime).IsRequired().HasMaxLength(10);
            b.Property(x => x.WorkEndTime).IsRequired().HasMaxLength(10);

            // Indexes
            b.HasIndex(x => x.UserId).IsUnique();

            // Relationships
            b.HasOne<IdentityUser>()
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

    }
}
