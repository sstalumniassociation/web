using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.Core.Entities.MembershipPlans;

namespace SSTAlumniAssociation.Core.Context;

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
    /// User revocations
    /// </summary>
    public DbSet<UserRevocation> UserRevocations { get; set; }
    
    /// <summary>
    /// Manual member approvals
    /// </summary>
    public DbSet<ManualMemberApproval> ManualMemberApprovals { get; set; }

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

        modelBuilder.HasPostgresEnum<ServiceAccountType>();
        modelBuilder.HasPostgresEnum<PaymentIntentState>();

        modelBuilder.Entity<MembershipPlan>()
            .HasData(
                DefaultMembershipPlans.Exco,
                DefaultMembershipPlans.Associate,
                DefaultMembershipPlans.Affiliate,
                DefaultMembershipPlans.Ordinary
            );

        modelBuilder
            .Entity<ServiceAccount>()
            .Property(u => u.ServiceAccountType)
            .HasConversion<string>();

        var qinGuan = new SystemAdmin()
        {
            Id = Guid.Parse("df90f5ea-a236-413f-a6c1-ca9197427631"),
            Name = "Qin Guan",
            FirebaseId = "GuZZVeOdlhNsf5dZGQmU2yV1Ox33",
            Email = "qinguan20040914@gmail.com",
        };

        modelBuilder.Entity<SystemAdmin>()
            .HasData(qinGuan);

        var guardHouse = new ServiceAccount
        {
            Id = Guid.Parse("a78a112f-3355-499e-aafd-824c14858b34"),
            Name = "Guard House Laptop",
            Email = "guardhouse+laptop@sstaa.org",
            FirebaseId = "EGjzDDZgHxXd80aGUYkeoP5fNnC2"
        };

        modelBuilder.Entity<ServiceAccount>()
            .HasData(guardHouse);

        var homecoming = new Event
        {
            Id = Guid.Parse("535bb727-f3eb-4cb3-adcf-aef04f14e82a"),
            Name = "Homecoming 2024",
            Description = "A very warm homecoming.",
            Location = "SST Multi-Purpose Hall",
            BadgeImage = "https://picsum.photos/200",
            StartDateTime = DateTime.UnixEpoch.AddSeconds(1705136400),
            EndDateTime = DateTime.UnixEpoch.AddSeconds(1705136400).AddHours(6),
            Active = true
        };

        var c14Reunion = new Event
        {
            Id = Guid.Parse("f2c84690-0311-4563-9635-ad0982cc1229"),
            Name = "Class of 2014 Reunion",
            Description = "Class of 2014 Reunion",
            Location = "HIGHfive @ 40 Sam Leong Road",
            BadgeImage = "https://picsum.photos/200",
            StartDateTime = DateTime.UnixEpoch.AddSeconds(1723888800),
            EndDateTime = DateTime.UnixEpoch.AddSeconds(1723888800).AddHours(6),
            Active = false
        };

        modelBuilder.Entity<Event>()
            .HasData(homecoming);

        modelBuilder.Entity<Event>()
            .HasData(c14Reunion);

        modelBuilder.Entity<Attendee>()
            .HasData(
                new Attendee
                {
                    Id = Guid.Parse("66e832ab-e00e-48aa-ba66-cb62f5dab6c0"),
                    UserId = qinGuan.Id,
                    EventId = homecoming.Id
                }
            );

        modelBuilder.Entity<Attendee>()
            .HasData(
                new Attendee
                {
                    Id = Guid.Parse("21ace349-7df6-49af-a204-9581c6cf017b"),
                    UserId = qinGuan.Id,
                    EventId = c14Reunion.Id
                }
            );

        modelBuilder.Entity<UserCheckIn>()
            .HasData(
                new UserCheckIn
                {
                    Id = Guid.Parse("d96a21d0-81dc-4d65-aa7a-020af478a849"),
                    CheckInDateTime = DateTime.UnixEpoch.AddSeconds(1723888800).AddMinutes(15),
                    ServiceAccountId = guardHouse.Id,
                    UserId = qinGuan.Id
                }
            );

        modelBuilder.Entity<GuestCheckIn>()
            .HasData(
                new GuestCheckIn
                {
                    Id = Guid.Parse("e15ae42b-b986-4fc6-b116-e7db6b213339"),
                    CheckInDateTime = DateTime.UnixEpoch.AddSeconds(1723888800).AddMinutes(30),
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