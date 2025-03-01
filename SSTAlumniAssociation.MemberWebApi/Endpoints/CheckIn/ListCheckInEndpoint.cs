using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Extensions;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.CheckIn;

public class ListCheckInEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<List<CheckInResponse>>
{
    public override void Configure()
    {
        Get("/CheckIn");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await dbContext.Users
            .WhereUserMatchesEmailFromClaims(HttpContext.User.Claims)
            .Include(u => u.CheckIns)
            .SingleAsync(cancellationToken: ct);

        await SendOkAsync(user.CheckIns.Select(c => c.ToResponse()).ToList(), ct);
    }
}