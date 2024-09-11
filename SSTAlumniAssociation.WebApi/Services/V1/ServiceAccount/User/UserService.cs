using System.Security.Claims;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Protos.User.V1;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.WebApi.Authorization;
using SSTAlumniAssociation.WebApi.Authorization.OwnerOrAdmin;
using SSTAlumniAssociation.WebApi.Mappers;
using SSTAlumniAssociation.WebApi.Services.V1.User;

namespace SSTAlumniAssociation.WebApi.Services.V1.ServiceAccount.User;

/// <inheritdoc />
[AuthorizeServiceAccount]
public class ServiceAccountUserServiceV1(
    ILogger<UserServiceV1> logger,
    IAuthorizationService authorizationService,
    AppDbContext dbContext
) : UserService.UserServiceBase
{

    /// <inheritdoc />
    [AuthorizeAdmin]
    public override Task<ListUsersResponse> ListUsers(ListUsersRequest request, ServerCallContext context)
    {
        logger.LogDebug("Processing request to list users");

        return Task.FromResult(new ListUsersResponse
        {
            Users =
            {
                dbContext.Users.Select(u => u.ToGrpc())
            }
        });
    }

    /// <inheritdoc />
    public override async Task<Protos.User.V1.User> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var authorized = await authorizationService.AuthorizeAsync(
            context.GetHttpContext().User,
            request.Id,
            OwnerOrAdminOperations.Read
        );

        if (!authorized.Succeeded)
        {
            throw new RpcException(
                new Status(StatusCode.PermissionDenied, "User is not authorized to peak at others.")
            );
        }

        var user = await dbContext.Users.FindAsync(Guid.Parse(request.Id));
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User does not exist."));
        }

        return user.ToGrpc();
    }

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

        await dbContext.SaveChangesAsync();

        return user.ToGrpc();
    }

    /// <inheritdoc />
    [AuthorizeAdmin]
    public override async Task<Protos.User.V1.User> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var user = await TransformAndAddUserAsync(request.User);
        await dbContext.SaveChangesAsync();

        return user.ToGrpc();
    }

    /// <inheritdoc />
    [AuthorizeAdmin]
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

        await dbContext.SaveChangesAsync();

        return new BatchCreateUsersResponse
        {
            Users = { users.ToGrpc() }
        };
    }

    /// <inheritdoc />
    [AuthorizeAdmin]
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

        await dbContext.SaveChangesAsync();

        return user.ToGrpc();
    }

    /// <inheritdoc />
    [AuthorizeAdmin]
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