using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.User;

public class GetUserEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<UserResponse>
{
    public override void Configure()
    {
        Get("/User/{Id:guid}");
        Policies(Authorization.Policies.ServiceAccount);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("Id");
        var user = await dbContext.Users.SingleAsync(u => u.Id == userId, cancellationToken: ct);

        await SendOkAsync(user.ToResponse(), ct);
    }
}