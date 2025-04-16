using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
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
            .SingleOrDefaultAsync(u => u.Email == context.User.Claims.GetEmail());

        if (user is { Revoked: false })
        {
            context.Succeed(requirement);
        }
    }
}