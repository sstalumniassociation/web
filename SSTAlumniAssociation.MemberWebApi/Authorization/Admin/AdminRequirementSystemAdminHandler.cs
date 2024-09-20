using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Extensions;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.Admin;

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