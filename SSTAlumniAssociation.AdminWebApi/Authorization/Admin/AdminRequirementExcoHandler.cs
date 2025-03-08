using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
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
            .WhereUserMatchesEmailFromClaims(context.User.Claims)
            .SingleOrDefaultAsync();

        if (sa is null)
        {
            context.Fail();
            return;
        }

        var activeSubscription = sa.Subscriptions.SingleOrDefault(s =>
            s.StartDateTime <= DateTime.Now &&
            s.EndDateTime >= DateTime.Now &&
            s.PaymentIntentState == PaymentIntentState.Success &&
            s.MembershipPlanId == DefaultMembershipPlans.Exco.Id
        );

        if (activeSubscription is null)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}