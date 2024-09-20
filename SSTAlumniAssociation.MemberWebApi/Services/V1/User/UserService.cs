using System.Security.Claims;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Protos.User.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Services.V1.User;

/// <inheritdoc />
public class UserService(
    ILogger<UserService> logger,
    IAuthorizationService authorizationService,
    AppDbContext dbContext
) : Users.UsersBase
{
    /// <inheritdoc />
    public override async Task<Protos.User.V1.User> BindUser(BindUserRequest request, ServerCallContext context)
    {
        var email = context.GetHttpContext().User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
        if (email is null)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Email does not exist in token."));
        }

        var firebaseId = context.GetHttpContext().User.Claims.SingleOrDefault(c => c.Type == "user_id");
        if (firebaseId is null)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Firebase ID does not exist in token."));
        }

        if (!Guid.TryParse(request.Id, out var userId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid user ID provided."));
        }

        var user = await dbContext.Users.FindAsync(userId);
        if (user is null || user.Email != email.Value)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Permission denied."));
        }

        if (!string.IsNullOrWhiteSpace(user.FirebaseId))
        {
            throw new RpcException(new Status(StatusCode.FailedPrecondition, "Firebase ID already set for user."));
        }

        user.FirebaseId = firebaseId.Value;

        await dbContext.SaveChangesAsync(context.CancellationToken);

        return user.ToGrpc();
    }
}