namespace SSTAlumniAssociation.Core.Entities.MembershipPlans;

public static class DefaultMembershipPlans
{
    public static MembershipPlan Exco = new()
    {
        Id = Guid.Parse("7ad2dfda-82df-4597-a76f-40e5fd4fd28d"),
        Name = "EXCO",
        BuiltIn = true,
        Description = "SSTAA EXCO",
        Duration = TimeSpan.FromDays(365),
        Price = 0
    };

    public static MembershipPlan Associate = new()
    {
        Id = Guid.Parse("c28780c6-d687-4bb8-b9ce-5fbca1e347c2"),
        Name = "Associate",
        BuiltIn = true,
        Description =
            "All past/present staff and students who completed at least 1 year of study in SST but did not graduate",
        Duration = TimeSpan.FromDays(365),
        Price = 0
    };

    public static MembershipPlan Affiliate = new()
    {
        Id = Guid.Parse("d258488b-c5a3-4f96-add7-366be4934900"),
        Name = "Affiliate",
        BuiltIn = true,
        Description = "All graduated alumni who are under 21",
        Duration = TimeSpan.FromDays(365),
        Price = 0
    };

    public static MembershipPlan Ordinary = new()
    {
        Id = Guid.Parse("c1869b12-56a9-4ed8-96d2-ef962c39799e"),
        Name = "Ordinary",
        BuiltIn = true,
        Description = "Ordinary",
        Duration = TimeSpan.FromDays(365),
        Price = 0
    };
}