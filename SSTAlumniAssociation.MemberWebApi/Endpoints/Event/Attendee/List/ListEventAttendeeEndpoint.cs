using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Endpoints.Attendee.Get;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Event.Attendee.List;

public class ListEventAttendeeEndpoint(AppDbContext dbContext)
    : Endpoint<ListEventAttendeeRequest, IEnumerable<AttendeeResponse>>
{
    public override void Configure()
    {
        Get("/Event/{Id:guid}/Attendee");
    }

    public override async Task HandleAsync(ListEventAttendeeRequest req, CancellationToken ct)
    {
        var @event = await dbContext.Events
            .Include(e => e.Attendees)
            .ThenInclude(a => a.User)
            .SingleOrDefaultAsync(e => e.Id == req.Id, cancellationToken: ct);

        if (@event is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(@event.Attendees.ToResponse(), cancellation: ct);
    }
}