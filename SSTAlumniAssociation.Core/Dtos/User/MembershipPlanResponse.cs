namespace SSTAlumniAssociation.Core.Dtos.User;

public class MembershipPlanResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public bool BuiltIn { get; set; }
    public string Description { get; set; }
    public TimeSpan Duration { get; set; }
    public decimal Price { get; set; }
}