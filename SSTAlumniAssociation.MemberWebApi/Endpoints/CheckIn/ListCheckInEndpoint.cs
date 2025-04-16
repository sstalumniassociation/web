using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Extensions;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.CheckIn;

public class ListCheckInEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<IEnumerable<CheckInResponse>>
{
    public override void Configure()
    {
        Get("/CheckIn");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var checkIns = dbContext.UserCheckIns
            .Include(u => u.User)
            .Where(c => c.User.Email == User.Claims.GetEmail());

        await SendOkAsync(checkIns.Select(c => c.ToResponse()), ct);
    }
}