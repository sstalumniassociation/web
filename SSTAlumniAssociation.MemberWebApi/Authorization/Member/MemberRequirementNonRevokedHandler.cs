using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.Core.Extensions;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.Member;

/// <inheritdoc cref="MemberRequirement" />
public class MemberRequirementNonRevokedHandler(AppDbContext dbContext) : AuthorizationHandler<MemberRequirement>
{
    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MemberRequirement requirement
    )
    {
        var user = await dbContext.Users
            .Include(u => u.Revocations)
            .Where(u => u.Id == context.User.Claims.GetNameIdentifierGuid())
            .SingleOrDefaultAsync();

        if (user is { Revoked: false })
        {
            context.Succeed(requirement);
        }
    }
}