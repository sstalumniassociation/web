using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Get;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee.List;

public class ListEventAttendeeEndpoint(AppDbContext dbContext)
    : Endpoint<ListEventAttendeeRequest, IEnumerable<AttendeeResponse>>
{
    public override void Configure()
    {
        Get("/Event/{Id:guid}/Attendee");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(ListEventAttendeeRequest req, CancellationToken ct)
    {
        var @event = await dbContext.Events
            .Include(e => e.Attendees)
            .SingleOrDefaultAsync(e => e.Id == req.Id, cancellationToken: ct);
        
        if (@event is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(@event.Attendees.ToResponse(), cancellation: ct);
    }
}