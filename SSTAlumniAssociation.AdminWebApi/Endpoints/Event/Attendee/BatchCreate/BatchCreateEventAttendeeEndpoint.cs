using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee.BatchCreate;

public class BatchCreateEventAttendeeEndpoint(AppDbContext dbContext)
    : Endpoint<BatchCreateEventAttendeeRequest, IEnumerable<AttendeeResponse>>
{
    public override void Configure()
    {
        Post("/Event/{Id:guid}/Attendee:Batch");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(BatchCreateEventAttendeeRequest req, CancellationToken ct)
    {
        var @event = await dbContext.Events
            .Include(e => e.Attendees)
            .SingleOrDefaultAsync(e => e.Id == req.Id, cancellationToken: ct);

        if (@event is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        foreach (var id in req.UserIds)
        {
            @event.Attendees.Add(new Core.Entities.Attendee
            {
                UserId = id
            });
        }

        await dbContext.SaveChangesAsync(ct);
    }
}