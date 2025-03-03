using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.CheckIn;

public class CreateCheckOutEndpoint(AppDbContext dbContext) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/CheckIn/{Id:guid}/CheckOut");
        Policies(Authorization.Policies.ServiceAccount);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("Id");

        var checkIn = await dbContext.CheckIns.SingleOrDefaultAsync(c => c.Id == id, cancellationToken: ct);
        if (checkIn is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        checkIn.CheckOutDateTime = DateTime.Now;

        await dbContext.SaveChangesAsync(ct);
    }
}