using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.Event.V1;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Services.V1.Event;

/// <inheritdoc />
public class EventService(
    AppDbContext dbContext,
    IAuthorizationService authorizationService
) : Events.EventsBase
{
    /// <inheritdoc />
    public override Task<ListEventsResponse> ListEvents(ListEventsRequest request, ServerCallContext context)
    {
        return Task.FromResult(
            new ListEventsResponse
            {
                Events =
                {
                    dbContext.Events.ToGrpc()
                }
            }
        );
    }

    public override async Task<Protos.Event.V1.Event> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        var @event = await dbContext.Events.FindAsync(Guid.Parse(request.Id));
        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Event does not exist."));
        }

        return @event.ToGrpc();
    }

    public override async Task<ListEventAttendeesResponse> ListEventAttendees(ListEventAttendeesRequest request,
        ServerCallContext context)
    {
        var @event = await dbContext.Events
            .Include(e => e.Attendees)
            .SingleOrDefaultAsync(e => e.Id == Guid.Parse(request.Id));

        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Event does not exist."));
        }

        return new ListEventAttendeesResponse
        {
            Attendees =
            {
                @event.Attendees.ToGrpc()
            }
        };
    }

    public override async Task<Protos.Event.V1.Event> CreateEvent(CreateEventRequest request, ServerCallContext context)
    {
        var entity = request.Event.ToEntity();
        var @event = await dbContext.Events.AddAsync(entity);

        await dbContext.SaveChangesAsync(context.CancellationToken);

        return @event.Entity.ToGrpc();
    }

    public override async Task<Protos.Event.V1.Event> UpdateEvent(UpdateEventRequest request, ServerCallContext context)
    {
        var @event = await dbContext.Events.FindAsync(Guid.Parse(request.Id));
        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        var proposedEventGrpc = @event.ToGrpc();
        request.UpdateMask.Merge(request.Event, proposedEventGrpc);
        var proposedEvent = proposedEventGrpc.ToEntity();

        if (proposedEvent.Id != @event.Id)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "ID cannot be modified."));
        }

        var entry = dbContext.Entry(@event);
        entry.CurrentValues.SetValues(proposedEvent);

        await dbContext.SaveChangesAsync(context.CancellationToken);

        return entry.Entity.ToGrpc();
    }

    public override async Task<Empty> DeleteEvent(DeleteEventRequest request, ServerCallContext context)
    {
        var @event = await dbContext.Events.FindAsync(Guid.Parse(request.Id));
        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        dbContext.Events.Remove(@event);
        await dbContext.SaveChangesAsync(context.CancellationToken);

        return new Empty();
    }

    public override async Task<Protos.Event.V1.Event> AddAttendee(AddAttendeeRequest request, ServerCallContext context)
    {
        var @event = await dbContext.Events.FindAsync(Guid.Parse(request.Id));
        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        var user = await dbContext.Users.FindAsync(Guid.Parse(request.User));
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User does not exist."));
        }

        @event.Attendees.Add(
            new Attendee
            {
                User = user
            }
        );

        await dbContext.SaveChangesAsync(context.CancellationToken);

        return @event.ToGrpc();
    }

    public override async Task<Protos.Event.V1.Event> BatchAddAttendees(BatchAddAttendeesRequest request,
        ServerCallContext context)
    {
        var @event = await dbContext.Events.FindAsync(Guid.Parse(request.Id));
        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        var userIds = request.Users.Select(Guid.Parse);
        foreach (var user in dbContext.Users.Where(u => userIds.Contains(u.Id)))
        {
            user.UserEvents.Add(
                new Attendee
                {
                    Event = @event
                }
            );
        }

        await dbContext.SaveChangesAsync(context.CancellationToken);
        return @event.ToGrpc();
    }
}