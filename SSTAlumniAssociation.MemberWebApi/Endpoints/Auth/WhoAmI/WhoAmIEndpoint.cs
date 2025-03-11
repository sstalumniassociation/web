using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.Core.Extensions;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Auth.WhoAmI;

public class WhoAmIEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<Ok<UserResponse>>
{
    public override void Configure()
    {
        Get("/Auth/WhoAmI");
    }

    public override async Task<Ok<UserResponse>> ExecuteAsync(CancellationToken ct)
    {
        var user = await dbContext.Users
            .Include(u => ((Member)u).Subscriptions)
            .ThenInclude(m => m.MembershipPlan)
            .SingleAsync(u => u.Email == User.Claims.GetEmail(), cancellationToken: ct);

        return TypedResults.Ok(user.ToResponse());
    }
}