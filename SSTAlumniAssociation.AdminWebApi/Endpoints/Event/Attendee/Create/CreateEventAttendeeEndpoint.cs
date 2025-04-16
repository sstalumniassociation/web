using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee.Create;

public class CreateEventAttendeeEndpoint(AppDbContext dbContext)
    : Endpoint<CreateEventAttendeeRequest, AttendeeResponse>
{
    public override void Configure()
    {
        Post("/Event/{Id:guid}/Attendee");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CreateEventAttendeeRequest req, CancellationToken ct)
    {
        var @event = await dbContext.Events
            .Include(e => e.Attendees)
            .SingleOrDefaultAsync(e => e.Id == req.Id, cancellationToken: ct);

        if (@event is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        @event.Attendees.Add(new Core.Entities.Attendee
        {
            UserId = req.UserId
        });

        await dbContext.SaveChangesAsync(ct);
    }
}