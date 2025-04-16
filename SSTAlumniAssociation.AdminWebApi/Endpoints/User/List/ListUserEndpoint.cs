using System.Collections.Immutable;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.List;

public class ListUserEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<IList<UserResponse>>
{
    public override void Configure()
    {
        Get("/User");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await dbContext.Users
            .Include(u => ((Member)u).Subscriptions)
            .ThenInclude(m => m.MembershipPlan)
            .ToListAsync(cancellationToken: ct);
        
        await SendOkAsync(users.ToResponse().ToImmutableList(), ct);
    }
}
