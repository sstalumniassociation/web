using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.Event.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Authorization;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Services.V1.Event;

/// <inheritdoc />
public class EventService(AppDbContext dbContext, IAuthorizationService authorizationService)
    : Events.EventsBase
{
    /// <inheritdoc />
    public override async Task<ListEventsResponse> ListEvents(ListEventsRequest request, ServerCallContext context)
    {
        return new ListEventsResponse
        {
            Events =
            {
                dbContext.Events.Where(e => e.Active).ToGrpc()
            }
        };
    }

    /// <inheritdoc />
    public override async Task<Protos.Event.V1.Event> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        var result = await dbContext.Events.SingleOrDefaultAsync(e => e.Id == Guid.Parse(request.Id) && e.Active);
        if (result is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        return result.ToGrpc();
    }
}