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
    public DbSet<UserEvent> UserEvents { get; set; }

    /// <summary>
    /// Articles
    /// </summary>
    public DbSet<Article> Articles { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().UseTptMappingStrategy();
        // modelBuilder.Entity<AuditRecord>().UseTpcMappingStrategy();
        
        // Store enum value as string to reduce risk of breaking changes vs storing as an int.
        modelBuilder
            .Entity<Member>()
            .Property(u => u.Membership)
            .HasConversion<string>();

        modelBuilder.Entity<AlumniMember>()
            .HasData(
                new AlumniMember
                {
                    Id = Guid.Parse("df90f5ea-a236-413f-a6c1-ca9197427631"),
                    Name = "Qin Guan",
                    FirebaseId = "GuZZVeOdlhNsf5dZGQmU2yV1Ox33",
                    Email = "qinguan20040914@gmail.com",
                    GraduationYear = 2000,
                    Membership = Membership.Exco,
                    MemberId = "EXCO-1",
                    Events = [],
                    UserEvents = []
                }
            );

        modelBuilder.Entity<AlumniMember>()
            .HasData(
                new AlumniMember
                {
                    Id = Guid.Parse("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"),
                    Name = "Tan Zheng Jie",
                    FirebaseId = "5ZPERFPTvfMfxwhH7SGsOmXqSco2",
                    Email = "tan_zheng_jie@sstaa.org",
                    Membership = Membership.Exco,
                    MemberId = "EXCO-1",
                    Events = [],
                    UserEvents = [],
                    Groups = []
                }
            );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}