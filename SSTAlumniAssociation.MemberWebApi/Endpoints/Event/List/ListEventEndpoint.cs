using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Event.List;

public class ListEventEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<IEnumerable<EventResponse>>
{
    public override void Configure()
    {
        Get("/Event");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var events = await dbContext.Events.Where(e => e.Active).ToListAsync(cancellationToken: ct);
        await SendAsync(events.ToResponse(), cancellation: ct);
    }
}
