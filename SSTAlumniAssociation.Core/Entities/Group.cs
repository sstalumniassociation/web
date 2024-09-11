namespace SSTAlumniAssociation.Core.Entities;

/// <summary>
/// A group represents any collection of users, for example, a Special Interest Group (SIG)
/// </summary>
public class Group
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    // Navigations
    
    public ICollection<Member> Members { get; } = [];
}