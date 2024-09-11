using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Extensions;

namespace SSTAlumniAssociation.MemberWebApi.Authorization;

public class SystemAdminAuthorizationHandler(AppDbContext dbContext): AuthorizationHandler<SystemAdminRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SystemAdminRequirement requirement)
    {
        var userId = context.User.Claims.GetNameIdentifierGuid();
        if (await dbContext.SystemAdmins.FindAsync(userId) is null)
        {
            context.Fail();
            return;
        }
        
        context.Succeed(requirement);
    }
}