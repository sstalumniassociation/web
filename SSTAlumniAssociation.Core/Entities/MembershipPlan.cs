namespace SSTAlumniAssociation.Core.Entities;

public class MembershipPlan
{
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the plan
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Built in plan that cannot be modified
    /// </summary>
    public required bool BuiltIn { get; set; }

    /// <summary>
    /// Description of the plan
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Duration of the plan
    /// </summary>
    public required TimeSpan Duration { get; set; }

    /// <summary>
    /// Price per duration of the plan
    /// </summary>
    public required decimal Price { get; set; }
}