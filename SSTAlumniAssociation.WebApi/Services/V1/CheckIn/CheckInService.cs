using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.CheckIn.V1;
using SSTAlumniAssociation.WebApi.Authorization;
using SSTAlumniAssociation.WebApi.Context;
using SSTAlumniAssociation.WebApi.Entities;
using SSTAlumniAssociation.WebApi.Extensions;
using SSTAlumniAssociation.WebApi.Mappers;

namespace SSTAlumniAssociation.WebApi.Services.V1.CheckIn;

[Authorize]
public class CheckInServiceV1(AppDbContext dbContext) : CheckInService.CheckInServiceBase
{
    [AuthorizeAdmin]
    public override async Task<ListCheckInsResponse> ListCheckIns(ListCheckInsRequest request,
        ServerCallContext context)
    {
        var query = dbContext.CheckIns.AsQueryable();

        if (request.CheckedOut is not null)
        {
            query = request.CheckedOut == true
                ? query.Where(c => c.CheckOutDateTime != null)
                : query.Where(c => c.CheckOutDateTime == null);
        }

        return new ListCheckInsResponse
        {
            CheckIns =
            {
                query.ToGrpc()
            }
        };
    }

    // TODO : permission checking
    public override async Task<Protos.CheckIn.V1.CheckIn> CreateCheckIn(CreateCheckInRequest request,
        ServerCallContext context)
    {
        switch (request.CheckIn.CheckInTypeCase)
        {
            case CheckInSimple.CheckInTypeOneofCase.Guest:
            {
                var proposedRecord = request.CheckIn.ToGuestCheckIn();

                proposedRecord.ServiceAccountId = context.GetHttpContext().User.Claims.GetNameIdentifierGuid();
                
                var record = await dbContext.GuestCheckIns.AddAsync(proposedRecord);
                await dbContext.SaveChangesAsync();

                return record.Entity.ToGrpc();
            }

            case CheckInSimple.CheckInTypeOneofCase.User:
            {
                var user = await dbContext.Users.Include(u => u.CheckIns).SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.CheckIn.User));
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

    // TODO : permission checking
    public override async Task<Protos.CheckIn.V1.CheckIn> CheckOut(CheckOutRequest request, ServerCallContext context)
    {
        var record = await dbContext.CheckIns.FindAsync(Guid.Parse(request.Id));
        if (record is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found."));
        }
        
        record.CheckOutDateTime = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        return record.ToGrpc();
    }
}