using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.Admin;

/// <inheritdoc cref="AdminRequirement"/>
public class AdminExcoHandler : AuthorizationHandler<AdminRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        context.Fail();
        return Task.CompletedTask;
    }
}