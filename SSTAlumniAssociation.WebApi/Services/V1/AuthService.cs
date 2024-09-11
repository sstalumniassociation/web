using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.Auth.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.WebApi.Extensions;
using SSTAlumniAssociation.WebApi.Mappers;

namespace SSTAlumniAssociation.WebApi.Services.V1;

/// <inheritdoc />
[Authorize]
public class AuthServiceV1(AppDbContext dbContext) : AuthService.AuthServiceBase
{
    /// <inheritdoc />
    [AllowAnonymous]
    public override async Task<VerifyUserResponse> VerifyUser(VerifyUserRequest request, ServerCallContext context)
    {
        var user = await dbContext.Users.Where(u => u.Email == request.Email).SingleOrDefaultAsync();
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User does not exist."));
        }

        return new VerifyUserResponse
        {
            Id = user.Id.ToString(),
            Linked = !string.IsNullOrWhiteSpace(user.FirebaseId)
        };
    }

    /// <inheritdoc />
    public override async Task<Protos.User.V1.User> WhoAmI(WhoAmIRequest request, ServerCallContext context)
    {
        var id = context.GetHttpContext().User.Claims.GetNameIdentifierGuid();

        var user = await dbContext.Users.FindAsync(id);
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User does not exist."));
        }
        
        return user.ToGrpc();
    }
}