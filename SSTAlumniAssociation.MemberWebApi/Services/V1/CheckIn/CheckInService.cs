using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.CheckIn.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.MemberWebApi.Authorization;
using SSTAlumniAssociation.MemberWebApi.Extensions;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Services.V1.CheckIn;

[Authorize]
public class CheckInServiceV1(
    AppDbContext dbContext,
    IAuthorizationService authorizationService
) : CheckInService.CheckInServiceBase
{
    public override async Task<ListCheckInsResponse> ListCheckIns(
        ListCheckInsRequest request,
        ServerCallContext context
    )
    {
        switch (request.Scope)
        {
            case "admin":
            {
                var isAdmin = await authorizationService.AuthorizeAsync(
                    context.GetHttpContext().User,
                    Policies.Admin
                );

                if (!isAdmin.Succeeded)
                {
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "Unauthorized"));
                }

                var query = dbContext.CheckIns
                    .Include(c => ((UserCheckIn)c).User)
                    .AsQueryable();

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
            case "app":
            default:
            {
                var userId = context.GetHttpContext().User.Claims.GetNameIdentifierGuid();
                var query = dbContext.UserCheckIns
                    .Include(c => c.User)
                    .Where(c => c.User.Id == userId);

                return new ListCheckInsResponse
                {
                    CheckIns =
                    {
                        query.ToGrpc()
                    }
                };
            }
        }
    }

    [AuthorizeServiceAccount]
    public override async Task<Protos.CheckIn.V1.CheckIn> CreateCheckIn(
        CreateCheckInRequest request,
        ServerCallContext context
    )
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

    [AuthorizeServiceAccount]
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