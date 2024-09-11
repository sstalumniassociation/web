using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.WebApi.Authorization.Member;

/// <inheritdoc cref="MemberRequirement" />
public class MemberNonRevokedHandler : AuthorizationHandler<MemberRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MemberRequirement requirement)
    {
        context.Fail();
        
        return Task.CompletedTask;
    }
}