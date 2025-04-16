using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
        var sa = await dbContext.ServiceAccounts
            .SingleOrDefaultAsync(u => u.Email == context.User.Claims.GetEmail());

        if (sa is not null)
        {
            context.Succeed(requirement);
        }
    }
}