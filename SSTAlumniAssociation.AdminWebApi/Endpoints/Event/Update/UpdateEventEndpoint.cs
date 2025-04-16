using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Update;

public class UpdateEventEndpoint(AppDbContext dbContext) : Endpoint<UpdateEventRequest, EventResponse>
{
    public override void Configure()
    {
        Post("/Event/{Id:guid}");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(UpdateEventRequest req, CancellationToken ct)
    {
        var @event = await dbContext.Events.SingleOrDefaultAsync(e => e.Id == req.Id, cancellationToken: ct);
        if (@event is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        @event.Name = req.Name;
        @event.Description = req.Description;
        @event.Location = req.Location;
        @event.BadgeImage = req.BadgeImage;
        @event.Active = req.Active;
        @event.StartDateTime = req.StartDateTime;
        @event.EndDateTime = req.EndDateTime;

        await dbContext.SaveChangesAsync(ct);
    }
}