using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities.MembershipPlans;
using SSTAlumniAssociation.Core.Extensions;

namespace SSTAlumniAssociation.AdminWebApi.Authorization.Admin;

/// <inheritdoc cref="AdminRequirement" />
public class AdminRequirementSystemAdminHandler(AppDbContext dbContext) : AuthorizationHandler<AdminRequirement>
{
    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminRequirement requirement
    )
    {
        var userId = context.User.Claims.GetNameIdentifierGuid();
        var sa = await dbContext.SystemAdmins.FindAsync(userId);
        if (sa is not null)
        {
            context.Succeed(requirement);
        }
    }
}