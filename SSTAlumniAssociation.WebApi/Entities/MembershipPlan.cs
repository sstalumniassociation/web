namespace SSTAlumniAssociation.WebApi.Entities;

public class MembershipPlan
{
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the plan
    /// </summary>
    public required string Name { get; set; }

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
    public required double Price { get; set; }
}