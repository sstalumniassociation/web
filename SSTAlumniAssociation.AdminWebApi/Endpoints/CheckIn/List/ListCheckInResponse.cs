using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.AdminWebApi.Endpoints.Article;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.CheckIn.List;

public class ListCheckInResponse(AppDbContext dbContext) : EndpointWithoutRequest<IEnumerable<CheckInResponse>>
{
    public override void Configure()
    {
        Get("/CheckIn");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var checkIns = await dbContext.CheckIns
            .Include(c => ((UserCheckIn)c).User)
            .ThenInclude(u => u.Revocations)
            .ToListAsync(cancellationToken: ct);

        await SendAsync(checkIns.ToResponse(), cancellation: ct);
    }
}
