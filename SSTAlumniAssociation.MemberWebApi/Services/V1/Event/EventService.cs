using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.Event.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Authorization;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Services.V1.Event;

[Authorize]
public class EventServiceV1(AppDbContext dbContext, IAuthorizationService authorizationService)
    : EventService.EventServiceBase
{
    /// <inheritdoc />
    public override async Task<ListEventsResponse> ListEvents(ListEventsRequest request, ServerCallContext context)
    {
        var isAdmin = await authorizationService.AuthorizeAsync(
            context.GetHttpContext().User,
            Policies.Admin
        );

        var query = dbContext.Events.AsQueryable();

        if (!isAdmin.Succeeded)
        {
            query = query.Where(e => e.Active == true);
        }

        return new ListEventsResponse
        {
            Events =
            {
                query.ToGrpcSimpleEvent()
            }
        };
    }

    public override async Task<Protos.Event.V1.Event> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        var isAdmin = await authorizationService.AuthorizeAsync(
            context.GetHttpContext().User,
            Policies.Admin
        );

        var query = dbContext.Events.Where(e => e.Id == Guid.Parse(request.Id));

        if (!isAdmin.Succeeded)
        {
            query = query.Where(e => e.Active == true);
        }
        else
        {
            query = query.Include(e => e.Attendees);
        }

        var result = await query.SingleOrDefaultAsync();
        if (result is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        return result.ToGrpcEvent();
    }

    [AuthorizeAdmin]
    public override async Task<Protos.Event.V1.EventSimple> CreateEvent(CreateEventRequest request,
        ServerCallContext context)
    {
        var entity = request.Event.ToEntity();
        var @event = await dbContext.Events.AddAsync(entity);
        return @event.Entity.ToGrpcSimpleEvent();
    }

    [AuthorizeAdmin]
    public override async Task<Protos.Event.V1.EventSimple> UpdateEvent(
        UpdateEventRequest request,
        ServerCallContext context
    )
    {
        var @event = await dbContext.Events.FindAsync(Guid.Parse(request.Id));
        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        var proposedEventGrpc = @event.ToGrpcSimpleEvent();
        request.UpdateMask.Merge(request.Event, proposedEventGrpc);
        var proposedEvent = proposedEventGrpc.ToEntity();

        if (proposedEvent.Id != @event.Id)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "ID cannot be modified."));
        }

        var entry = dbContext.Entry(@event);
        entry.CurrentValues.SetValues(proposedEvent);

        await dbContext.SaveChangesAsync();

        return entry.Entity.ToGrpcSimpleEvent();
    }

    [AuthorizeAdmin]
    public override async Task<Empty> DeleteEvent(DeleteEventRequest request, ServerCallContext context)
    {
        var @event = await dbContext.Events.FindAsync(Guid.Parse(request.Id));
        if (@event is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        dbContext.Events.Remove(@event);
        await dbContext.SaveChangesAsync();

        return new Empty();
    }

    public override async Task<Protos.Event.V1.Event> AddAttendee(AddAttendeeRequest request, ServerCallContext context)
    {
        return await base.AddAttendee(request, context);
    }

    [AuthorizeAdmin]
    public override async Task<Protos.Event.V1.Event> BatchAddAttendees(BatchAddAttendeesRequest request,
        ServerCallContext context)
    {
        return await base.BatchAddAttendees(request, context);
    }
}