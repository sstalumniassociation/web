using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.Event.V1;
using SSTAlumniAssociation.WebApi.Authorization;
using SSTAlumniAssociation.WebApi.Authorization.Admin;
using SSTAlumniAssociation.WebApi.Context;
using SSTAlumniAssociation.WebApi.Entities;
using SSTAlumniAssociation.WebApi.Mappers;
using Event = Protos.Event.V1.Event;

namespace SSTAlumniAssociation.WebApi.Services.V1;

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

    public override async Task<Event> GetEvent(GetEventRequest request, ServerCallContext context)
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

    public override async Task<Event> GetAdmission(GetAdmissionRequest request, ServerCallContext context)
    {
        return await base.GetAdmission(request, context);
    }

    public override async Task<Event> CreateEvent(CreateEventRequest request, ServerCallContext context)
    {
        return await base.CreateEvent(request, context);
    }

    public override async Task<Event> UpdateEvent(UpdateEventRequest request, ServerCallContext context)
    {
        return await base.UpdateEvent(request, context);
    }

    public override async Task<Empty> DeleteEvent(DeleteEventRequest request, ServerCallContext context)
    {
        return await base.DeleteEvent(request, context);
    }

    public override async Task<Event> AddAttendee(AddAttendeeRequest request, ServerCallContext context)
    {
        return await base.AddAttendee(request, context);
    }

    public override async Task<Event> BatchAddAttendees(BatchAddAttendeesRequest request, ServerCallContext context)
    {
        return await base.BatchAddAttendees(request, context);
    }
}