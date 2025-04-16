using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Delete;

public class DeleteEventEndpoint(AppDbContext dbContext) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/Event/{Id:guid}");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var eventId = Route<Guid?>("Id");
        if (!eventId.HasValue)
        {
            throw new ArgumentNullException(nameof(eventId));
        }

        var @event = await dbContext.Events.SingleAsync(u => u.Id == eventId, cancellationToken: ct);
        dbContext.Events.Remove(@event);

        await SendOkAsync(ct);
    }
}