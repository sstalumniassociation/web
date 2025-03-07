using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Dtos.User;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.List;

public class ListUserEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<List<UserResponse>>
{
    public override void Configure()
    {
        Get("/User");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await dbContext.Users.ToListAsync(cancellationToken: ct);
        await SendOkAsync(users.Select(u => u.ToResponse()).ToList(), ct);
    }
}
