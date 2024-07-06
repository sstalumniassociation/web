using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.WebApi.Authorization.Member;

/// <inheritdoc cref="MemberRequirement" />
public class MemberNonRevokedHandler : AuthorizationHandler<MemberRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MemberRequirement requirement)
    {
        var role = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

        if (role is not null && role.Value != Entities.Membership.Revoked.ToString())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}