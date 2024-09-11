using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Extensions;

namespace SSTAlumniAssociation.MemberWebApi.Authorization;

public class ExcoAuthorizationHandler(AppDbContext dbContext): AuthorizationHandler<ExcoRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ExcoRequirement requirement)
    {
        var userId = context.User.Claims.GetNameIdentifierGuid();
        if (await dbContext.Members.FindAsync(userId) is null)
        {
            context.Fail();
            return;
        }
        
        context.Succeed(requirement);
    }
}