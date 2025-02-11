using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.List;

public class ListEventEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<IEnumerable<EventResponse>>
{
    public override void Configure()
    {
        Get("/Event");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var events = await dbContext.Events.ToListAsync(cancellationToken: ct);
        await SendAsync(events.ToResponse(), cancellation: ct);
    }
}