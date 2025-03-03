using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.CheckIn;

public class CreateCheckInEndpoint(AppDbContext dbContext) : Endpoint<CreateCheckInRequest, CheckInResponse>
{
    public override void Configure()
    {
        Post("/CheckIn");
        Policies(Authorization.Policies.ServiceAccount);
    }

    public override async Task HandleAsync(CreateCheckInRequest req, CancellationToken ct)
    {
        var checkIn = req.ToEntity();

        switch (checkIn)
        {
            case GuestCheckIn guest:
            {
                guest.CheckInDateTime = DateTime.Now;
                var entity = await dbContext.GuestCheckIns.AddAsync(guest, ct);
                await dbContext.SaveChangesAsync(ct);

                await SendOkAsync(entity.Entity.ToResponse(), ct);

                break;
            }
            case UserCheckIn user:
            {
                var u = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == user.UserId, cancellationToken: ct);
                if (u is null)
                {
                    await SendNotFoundAsync(ct);
                    return;
                }

                if (u.Email != User.Claims.Single(c => c.Type == ClaimTypes.Email).Value)
                {
                    await SendUnauthorizedAsync(ct);
                    return;
                }

                var entity = new UserCheckIn
                {
                    CheckInDateTime = DateTime.Now
                };

                u.CheckIns.Add(entity);
                await dbContext.SaveChangesAsync(ct);

                await SendOkAsync(entity.ToResponse(), ct);

                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(req));
        }
    }
}