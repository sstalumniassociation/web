using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.User.V1;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Services.V1.User;

/// <inheritdoc />
public class UserService(
    ILogger<UserService> logger,
    IAuthorizationService authorizationService,
    AppDbContext dbContext
) : Users.UsersBase
{
    /// <inheritdoc />
    public override Task<ListUsersResponse> ListUsers(ListUsersRequest request, ServerCallContext context)
    {
        logger.LogDebug("Processing request to list users");

        return Task.FromResult(new ListUsersResponse
        {
            Users =
            {
                dbContext.Users.ToGrpc()
            }
        });
    }

    /// <inheritdoc />
    public override async Task<Protos.User.V1.User> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var user = await dbContext.Users.FindAsync(Guid.Parse(request.Id));
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User does not exist."));
        }

        return user.ToGrpc();
    }

    /// <inheritdoc />
    public override async Task<Protos.User.V1.User> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var user = await TransformAndAddUserAsync(request.User);
        await dbContext.SaveChangesAsync(context.CancellationToken);

        return user.ToGrpc();
    }

    /// <inheritdoc />
    public override async Task<BatchCreateUsersResponse> BatchCreateUsers(
        BatchCreateUsersRequest request,
        ServerCallContext context
    )
    {
        var users = new List<Core.Entities.User>();
        foreach (var user in request.Requests)
        {
            users.Add(await TransformAndAddUserAsync(user.User));
        }

        await dbContext.SaveChangesAsync(context.CancellationToken);

        return new BatchCreateUsersResponse
        {
            Users = { users.ToGrpc() }
        };
    }

    /// <inheritdoc />
    public override async Task<Protos.User.V1.User> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var user = await dbContext.Users.SingleAsync(c => c.Id.ToString() == request.User.Id);
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User disappeared into the abyss."));
        }

        var diff = new Protos.User.V1.User();
        request.UpdateMask.Merge(request.User, diff);

        if (request.UpdateMask.Paths.Contains("name"))
        {
            user.Name = diff.Name;
        }

        if (request.UpdateMask.Paths.Contains("email"))
        {
            user.Email = diff.Email;
        }

        if (request.UpdateMask.Paths.Contains("firebase_id"))
        {
            user.FirebaseId = diff.FirebaseId;
        }

        await dbContext.SaveChangesAsync(context.CancellationToken);

        return user.ToGrpc();
    }

    /// <inheritdoc />
    public override Task<Empty> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        throw new RpcException(new Status(StatusCode.Unimplemented, "Users cannot be deleted."));
    }

    private async Task<Core.Entities.User> TransformAndAddUserAsync(Protos.User.V1.User user)
    {
        switch (user.UserTypeCase)
        {
            case Protos.User.V1.User.UserTypeOneofCase.Employee:
            {
                var employee = user.ToEmployee();
                var entity = await dbContext.Employees.AddAsync(employee);
                return entity.Entity;
            }

            case Protos.User.V1.User.UserTypeOneofCase.Member:
            {
                switch (user.Member.MemberTypeCase)
                {
                    case Member.MemberTypeOneofCase.AlumniMember:
                    {
                        var alumniMember = user.ToAlumniMember();
                        var entity = await dbContext.AlumniMembers.AddAsync(alumniMember);
                        return entity.Entity;
                    }

                    case Member.MemberTypeOneofCase.EmployeeMember:
                    {
                        var employeeMember = user.ToEmployeeMember();
                        var entity = await dbContext.EmployeeMembers.AddAsync(employeeMember);
                        return entity.Entity;
                    }

                    case Member.MemberTypeOneofCase.None:
                    default:
                        throw new Exception("Unrecognized user type. An engineer was not careful enough.");
                }
            }

            case Protos.User.V1.User.UserTypeOneofCase.ServiceAccount:
            {
                var serviceAccount = user.ToServiceAccount();
                var entity = await dbContext.ServiceAccounts.AddAsync(serviceAccount);
                return entity.Entity;
            }

            case Protos.User.V1.User.UserTypeOneofCase.SystemAdmin:
            {
                var systemAdmin = user.ToSystemAdmin();
                var entity = await dbContext.SystemAdmins.AddAsync(systemAdmin);
                return entity.Entity;
            }

            case Protos.User.V1.User.UserTypeOneofCase.None:
            default:
                throw new Exception("Unrecognized user type. An engineer was not careful enough.");
        }
    }
}