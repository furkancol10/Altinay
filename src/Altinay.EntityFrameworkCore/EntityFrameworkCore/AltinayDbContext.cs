using Altinay.Meeting;
using Altinay.Personel;
using Altinay.Personel.Departments;
using Altinay.Personel.Managers;
using Altinay.Projects;
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

    public AltinayDbContext(DbContextOptions<AltinayDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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

    }
}
/*
b.Property(x => x.ProjectName).IsRequired().HasMaxLength(128);
b.Property(x => x.ProjectID).IsRequired();
b.Property(x => x.ProjectDescription).IsRequired(); */