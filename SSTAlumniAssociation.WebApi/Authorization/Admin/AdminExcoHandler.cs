using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.WebApi.Authorization.Admin;

/// <inheritdoc cref="AdminRequirement"/>
public class AdminExcoHandler : AuthorizationHandler<AdminRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        var role = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

        if (role is not null && role.Value == Membership.Exco.ToString())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}