using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities.MembershipPlans;
using SSTAlumniAssociation.Core.Extensions;

namespace SSTAlumniAssociation.AdminWebApi.Authorization.Admin;

/// <inheritdoc cref="AdminRequirement" />
public class AdminRequirementExcoHandler(AppDbContext dbContext) : AuthorizationHandler<AdminRequirement>
{
    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminRequirement requirement
    )
    {
        var sa = await dbContext.Members
            .Include(u => u.Subscriptions)
            .ThenInclude(s => s.MembershipPlan)
            .WhereUserMatchesEmailFromClaims(context.User.Claims)
            .Where(m => m.ActiveSubscription != null &&
                        m.ActiveSubscription.MembershipPlan.Id == DefaultMembershipPlans.Exco.Id
            )
            .SingleOrDefaultAsync();

        if (sa is not null)
        {
            context.Succeed(requirement);
        }
    }
}