using FastEndpoints;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Create;

public class CreateEventEndpoint(AppDbContext dbContext) : Endpoint<CreateEventRequest, EventResponse>
{
    public override void Configure()
    {
        Post("/Event");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CreateEventRequest req, CancellationToken ct)
    {
        var @event = await dbContext.Events.AddAsync(new Core.Entities.Event
        {
            Name = req.Name,
            Description = req.Description,
            Location = req.Location,
            BadgeImage = req.BadgeImage,
            Active = req.Active,
            StartDateTime = req.StartDateTime,
            EndDateTime = req.EndDateTime
        }, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendAsync(@event.Entity.ToResponse(), cancellation: ct);
    }
}