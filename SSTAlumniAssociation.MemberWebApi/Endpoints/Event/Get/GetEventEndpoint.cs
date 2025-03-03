using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Event.Get;

public class GetEventEndpoint(AppDbContext dbContext) : Endpoint<GetEventRequest, EventResponse>
{
    public override void Configure()
    {
        Get("/Event/{Id:guid}");
    }

    public override async Task HandleAsync(GetEventRequest req, CancellationToken ct)
    {
        var @event = await dbContext.Events.SingleOrDefaultAsync(e => e.Id == req.Id, cancellationToken: ct);
        if (@event is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        await SendAsync(@event.ToResponse(), cancellation: ct);
    }
}