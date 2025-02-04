using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.CheckIn.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Extensions;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Services.V1;

/// <inheritdoc />
public class CheckInService(
    AppDbContext dbContext,
    IAuthorizationService authorizationService
) : CheckIns.CheckInsBase
{
    /// <inheritdoc />
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
}