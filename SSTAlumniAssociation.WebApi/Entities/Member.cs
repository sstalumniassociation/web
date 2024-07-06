using Microsoft.EntityFrameworkCore;

namespace SSTAlumniAssociation.WebApi.Entities;

[Index(nameof(MemberId), IsUnique = true)]
public abstract class Member : User
{
    /// <summary>
    /// Internal member ID used for tracking by SSTAA admin.
    /// </summary>
    public required string MemberId { get; set; }
    public required Membership Membership { get; set; }
    
    // Navigations

    public List<Group> Groups { get; set; }
}