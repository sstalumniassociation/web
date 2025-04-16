using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.Delete;

public class DeleteUserEndpoint(AppDbContext dbContext) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/User/{Id:guid}");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid?>("Id");
        if (!userId.HasValue)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        var user = await dbContext.Users.SingleAsync(u => u.Id == userId, cancellationToken: ct);
        dbContext.Users.Remove(user);

        await SendOkAsync(ct);
    }
}