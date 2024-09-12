using Grpc.Core;
using Protos.User.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Services.V1;

public class UserService(AppDbContext dbContext) : Protos.User.V1.UserService.UserServiceBase
{
    public override async Task<User> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var user = await dbContext.Users.FindAsync(Guid.Parse(request.Id));
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User does not exist."));
        }

        return user.ToGrpc();
    }
}