using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.Core.Extensions;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.Member;

/// <inheritdoc cref="MemberRequirement" />
public class MemberRequirementSystemAdminHandler(AppDbContext dbContext) : AuthorizationHandler<MemberRequirement>
{
    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MemberRequirement requirement
    )
    {
        var user = await dbContext.SystemAdmins.FindAsync(context.User.Claims.GetNameIdentifierGuid());
        if (user is not null)
        {
            context.Succeed(requirement);
        }
    }
}