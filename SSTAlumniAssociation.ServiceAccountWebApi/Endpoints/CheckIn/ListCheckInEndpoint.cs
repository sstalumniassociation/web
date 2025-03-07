using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.CheckIn;

public class ListCheckInEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<List<CheckInResponse>>
{
    public override void Configure()
    {
        Get("/CheckIn");
        Policies(Authorization.Policies.ServiceAccount);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var checkIns = await dbContext.CheckIns.ToListAsync(cancellationToken: ct);

        await SendOkAsync(checkIns.Select(c => c.ToResponse()).ToList(), ct);
    }
}