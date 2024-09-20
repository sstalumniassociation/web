using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Extensions;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Authorization.ServiceAccount;

/// <inheritdoc cref="ServiceAccountRequirement" />
public class ServiceAccountHandler(AppDbContext dbContext) : AuthorizationHandler<ServiceAccountRequirement>
{
    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ServiceAccountRequirement requirement
    )
    {
        var userId = context.User.Claims.GetNameIdentifierGuid();
        var sa = await dbContext.ServiceAccounts.FindAsync(userId);
        if (sa is not null)
        {
            context.Succeed(requirement);
        }
    }
}