using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.CheckIn.V1;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Services.V1;

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
}