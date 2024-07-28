using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.Attendee.V1;
using SSTAlumniAssociation.WebApi.Context;
using SSTAlumniAssociation.WebApi.Mappers;

namespace SSTAlumniAssociation.WebApi.Services.V1;

public class AttendeeServiceV1(AppDbContext dbContext, IAuthorizationService authorizationService)
    : AttendeeService.AttendeeServiceBase
{
    public override async Task<Attendee> GetAttendee(GetAttendeeRequest request, ServerCallContext context)
    {
        var admission = await dbContext.Attendees
            .Include(a => a.User)
            .Include(a => a.Event)
            .SingleOrDefaultAsync(a => a.Id == Guid.Parse(request.Id) && a.Event.Active);

        if (admission is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        return admission.ToGrpc();
    }
}