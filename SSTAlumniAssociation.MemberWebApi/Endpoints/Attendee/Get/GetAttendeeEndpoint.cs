using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Attendee.Get;

public class GetAttendeeEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<AttendeeResponse>
{
    public override void Configure()
    {
        Get("/Attendee/{AttendeeId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var attendee = await dbContext.Attendees.SingleOrDefaultAsync(
            a => a.Id == Route<Guid>("AttendeeId", true),
            cancellationToken: ct
        );

        if (attendee is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(attendee.ToResponse(), ct);
    }
}