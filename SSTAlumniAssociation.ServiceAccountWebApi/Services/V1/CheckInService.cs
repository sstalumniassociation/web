using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.CheckIn.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.Core.Extensions;
using SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Services.V1;

public class CheckInService(
    AppDbContext dbContext,
    IAuthorizationService authorizationService
) : CheckIns.CheckInsBase
{
    public override async Task<ListCheckInsResponse> ListCheckIns(
        ListCheckInsRequest request,
        ServerCallContext context
    )
    {
        var user = await dbContext.Users
            .WhereUserMatchesEmailFromClaims(context.GetHttpContext().User.Claims)
            .Include(u => u.CheckIns)
            .SingleOrDefaultAsync();

        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }

        return new ListCheckInsResponse
        {
            CheckIns =
            {
                user.CheckIns.ToGrpc()
            }
        };
    }

    public override async Task<Protos.CheckIn.V1.CheckIn> CreateCheckIn(
        CreateCheckInRequest request,
        ServerCallContext context
    )
    {
        switch (request.CheckIn.CheckInTypeCase)
        {
            case Protos.CheckIn.V1.CheckIn.CheckInTypeOneofCase.Guest:
            {
                var proposedRecord = request.CheckIn.ToGuestCheckIn();

                proposedRecord.ServiceAccountId = context.GetHttpContext().User.Claims.Where();

                var record = await dbContext.GuestCheckIns.AddAsync(proposedRecord);
                await dbContext.SaveChangesAsync();

                return record.Entity.ToGrpc();
            }

            case Protos.CheckIn.V1.CheckIn.CheckInTypeOneofCase.User:
            {
                var user = await dbContext.Users.Include(u => u.CheckIns)
                    .SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.CheckIn.User));
                if (user is null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
                }

                if (user.CheckIns.Any(c => c.CheckOutDateTime is null))
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "User has not checked out."));
                }

                var proposedRecord = request.CheckIn.ToUserCheckIn();

                proposedRecord.ServiceAccountId = context.GetHttpContext().User.Claims.GetNameIdentifierGuid();
                proposedRecord.User = user;

                var record = await dbContext.UserCheckIns.AddAsync(proposedRecord);
                await dbContext.SaveChangesAsync();

                return record.Entity.ToGrpc();
            }

            case CheckInSimple.CheckInTypeOneofCase.None:
            default:
                throw new Exception("Invariant check in type case.");
        }
    }

    public override async Task<Protos.CheckIn.V1.CheckIn> CheckOut(CheckOutRequest request, ServerCallContext context)
    {
        var record = await dbContext.CheckIns
            .Include(c => ((UserCheckIn)c).User)
            .SingleOrDefaultAsync(c => c.Id == Guid.Parse(request.Id));

        if (record is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }

        if (record.CheckOutDateTime is not null)
        {
            throw new RpcException(new Status(StatusCode.FailedPrecondition, "User has already checked out."));
        }

        record.CheckOutDateTime = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        return record.ToGrpc();
    }
}