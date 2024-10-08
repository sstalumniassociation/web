using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.ServiceAccount;

/// <inheritdoc cref="ServiceAccountRequirement" />
public class ServiceAccountHandler: AuthorizationHandler<ServiceAccountRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ServiceAccountRequirement requirement)
    {
        var role = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

        if (role is not null && role.Value != nameof(Core.Entities.ServiceAccount))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}