using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.WebApi.Context;
using SSTAlumniAssociation.WebApi.Extensions;

namespace SSTAlumniAssociation.WebApi.Authorization;

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