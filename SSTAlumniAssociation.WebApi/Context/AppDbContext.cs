using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.WebApi.Entities;

namespace SSTAlumniAssociation.WebApi.Context;

/// <inheritdoc />
public partial class AppDbContext : DbContext
{
    /// <inheritdoc />
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Users
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Members
    /// </summary>
    public DbSet<Member> Members { get; set; }

    /// <summary>
    /// Alumni members
    /// </summary>
    public DbSet<AlumniMember> AlumniMembers { get; set; }

    /// <summary>
    /// Employee members
    /// </summary>
    public DbSet<EmployeeMember> EmployeeMembers { get; set; }

    /// <summary>
    /// Employees
    /// </summary>
    public DbSet<Employee> Employees { get; set; }

    /// <summary>
    /// Service accounts
    /// </summary>
    public DbSet<ServiceAccount> ServiceAccounts { get; set; }

    /// <summary>
    /// System admins
    /// </summary>
    public DbSet<SystemAdmin> SystemAdmins { get; set; }

    /// <summary>
    /// Events
    /// </summary>
    public DbSet<Event> Events { get; set; }

    /// <summary>
    /// Users to events
    /// </summary>
    public DbSet<Attendee> Attendees { get; set; }

    /// <summary>
    /// Articles
    /// </summary>
    public DbSet<Article> Articles { get; set; }

    /// <summary>
    /// All check ins
    /// </summary>
    public DbSet<CheckIn> CheckIns { get; set; }

    /// <summary>
    /// Registered user check ins
    /// </summary>
    public DbSet<UserCheckIn> UserCheckIns { get; set; }

    /// <summary>
    /// Guest check ins
    /// </summary>
    public DbSet<GuestCheckIn> GuestCheckIns { get; set; }

    /// <summary>
    /// Available membership plans
    /// </summary>
    public DbSet<MembershipPlan> MembershipPlans { get; set; }

    /// <summary>
    /// Membership subscriptions
    /// </summary>
    public DbSet<MembershipSubscription> MembershipSubscriptions { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().UseTptMappingStrategy();
        modelBuilder.Entity<CheckIn>().UseTptMappingStrategy();
        // modelBuilder.Entity<AuditRecord>().UseTpcMappingStrategy();

        var exco =
            new MembershipPlan
            {
                Id = Guid.NewGuid(),
                Name = "EXCO",
                BuiltIn = true,
                Description = "SSTAA EXCO",
                Duration = TimeSpan.FromDays(365),
                Price = 0
            };

        var associate = new MembershipPlan
        {
            Id = Guid.NewGuid(),
            Name = "Associate",
            BuiltIn = true,
            Description =
                "All past/present staff and students who completed at least 1 year of study in SST but did not graduate",
            Duration = TimeSpan.FromDays(365),
            Price = 0
        };

        var affiliate = new MembershipPlan
        {
            Id = Guid.NewGuid(),
            Name = "Affiliate",
            BuiltIn = true,
            Description = "All graduated alumni who are under 21",
            Duration = TimeSpan.FromDays(365),
            Price = 0
        };

        var ordinary = new MembershipPlan
        {
            Id = Guid.NewGuid(),
            Name = "Ordinary",
            BuiltIn = true,
            Description = "Ordinary",
            Duration = TimeSpan.FromDays(365),
            Price = 0
        };
        
        modelBuilder.Entity<MembershipPlan>()
            .HasData([exco, associate, affiliate, ordinary]);

        modelBuilder
            .Entity<ServiceAccount>()
            .Property(u => u.ServiceAccountType)
            .HasConversion<string>();

        var qinGuan = new AlumniMember
        {
            Id = Guid.Parse("df90f5ea-a236-413f-a6c1-ca9197427631"),
            Name = "Qin Guan",
            FirebaseId = "GuZZVeOdlhNsf5dZGQmU2yV1Ox33",
            Email = "qinguan20040914@gmail.com",
            GraduationYear = 2000,
            MemberId = "EXCO-1",
        };

        var qinGuanExco = new MembershipSubscription
        {
            Id = Guid.NewGuid(),
            StartDateTime = DateTime.UtcNow,
            EndDateTime = DateTime.UtcNow.AddDays(365),
            Member = qinGuan,
            MembershipPlan = exco
        };

        modelBuilder.Entity<AlumniMember>()
            .HasData(qinGuan);

        modelBuilder.Entity<MembershipSubscription>()
            .HasData(qinGuanExco);

        var guardHouse = new ServiceAccount
        {
            Id = Guid.Parse("a78a112f-3355-499e-aafd-824c14858b34"),
            Name = "Guard House Laptop",
            Email = "guardhouse+laptop@sstaa.org",
            FirebaseId = "EGjzDDZgHxXd80aGUYkeoP5fNnC2"
        };

        modelBuilder.Entity<ServiceAccount>()
            .HasData(guardHouse);

        var zhengJie = new AlumniMember
        {
            Id = Guid.Parse("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"),
            Name = "Tan Zheng Jie",
            FirebaseId = "5ZPERFPTvfMfxwhH7SGsOmXqSco2",
            Email = "tan_zheng_jie@sstaa.org",
            MemberId = "EXCO-2"
        };
        
        var zhengJieExco = new MembershipSubscription
        {
            Id = Guid.NewGuid(),
            StartDateTime = DateTime.UtcNow,
            EndDateTime = DateTime.UtcNow.AddDays(365),
            Member = zhengJie,
            MembershipPlan = exco
        };

        modelBuilder.Entity<AlumniMember>()
            .HasData(zhengJie);

        modelBuilder.Entity<MembershipSubscription>()
            .HasData(zhengJieExco);

        var homecoming = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Homecoming 2024",
            Description = "A very warm homecoming.",
            Location = "SST Multi-Purpose Hall",
            BadgeImage = "https://picsum.photos/200",
            StartDateTime = DateTime.UtcNow,
            EndDateTime = DateTime.UtcNow.AddDays(10),
            Active = true
        };

        var trial = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Trial",
            Description = "A very warm homecoming.",
            Location = "SST Multi-Purpose Hall",
            BadgeImage = "https://picsum.photos/200",
            StartDateTime = DateTime.UtcNow,
            EndDateTime = DateTime.UtcNow.AddDays(10),
            Active = false
        };

        modelBuilder.Entity<Event>()
            .HasData(homecoming);

        modelBuilder.Entity<Event>()
            .HasData(trial);

        modelBuilder.Entity<Attendee>()
            .HasData(
                new Attendee
                {
                    Id = Guid.NewGuid(),
                    UserId = qinGuan.Id,
                    EventId = homecoming.Id
                }
            );

        modelBuilder.Entity<Attendee>()
            .HasData(
                new Attendee
                {
                    Id = Guid.NewGuid(),
                    UserId = qinGuan.Id,
                    EventId = trial.Id
                }
            );

        modelBuilder.Entity<UserCheckIn>()
            .HasData(
                new UserCheckIn
                {
                    Id = Guid.NewGuid(),
                    CheckInDateTime = DateTime.UtcNow,
                    ServiceAccountId = guardHouse.Id,
                    UserId = qinGuan.Id
                }
            );

        modelBuilder.Entity<UserCheckIn>()
            .HasData(
                new UserCheckIn
                {
                    Id = Guid.NewGuid(),
                    CheckInDateTime = DateTime.UtcNow,
                    ServiceAccountId = guardHouse.Id,
                    UserId = zhengJie.Id
                }
            );

        modelBuilder.Entity<GuestCheckIn>()
            .HasData(
                new GuestCheckIn
                {
                    Id = Guid.NewGuid(),
                    CheckInDateTime = DateTime.UtcNow,
                    ServiceAccountId = guardHouse.Id,
                    Name = "Alex",
                    Nric = "999B",
                    Phone = "9999 9999",
                    Reason = "Alex is bored"
                }
            );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}