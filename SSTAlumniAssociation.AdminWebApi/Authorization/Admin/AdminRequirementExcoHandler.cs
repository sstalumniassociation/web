using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.Core.Entities.MembershipPlans;
using SSTAlumniAssociation.Core.Extensions;

namespace SSTAlumniAssociation.AdminWebApi.Authorization.Admin;

/// <inheritdoc cref="AdminRequirement" />
public class AdminRequirementExcoHandler(AppDbContext dbContext, ILogger<AdminRequirementExcoHandler> logger)
    : AuthorizationHandler<AdminRequirement>
{
    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminRequirement requirement
    )
    {
        var activeSubscription = await dbContext.MembershipSubscriptions
            .Include(s => s.Member)
            .SingleOrDefaultAsync(s =>
                s.Member.Email == context.User.Claims.GetEmail() &&
                s.StartDateTime <= DateTime.UtcNow &&
                s.EndDateTime >= DateTime.UtcNow &&
                s.PaymentIntentState == PaymentIntentState.Success &&
                s.MembershipPlanId == DefaultMembershipPlans.Exco.Id
            );

        if (activeSubscription is null)
        {
            logger.LogWarning("Admin requirement failed for user {Email}", context.User.Claims.GetEmail());
            context.Fail();
            return;
        }

        logger.LogInformation("Admin requirement succeeded for user {Email}", activeSubscription.Member.Email);
        context.Succeed(requirement);
    }
}
