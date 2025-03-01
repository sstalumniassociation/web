using FastEndpoints;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SSTAlumniAssociation.AdminWebApi.Endpoints.Event;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.Create;

public class ListUserEndpoint(AppDbContext dbContext) : Endpoint<CreateUserRequest, UserResponse>
{
    public override void Configure()
    {
        Get("/User");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var user = req.ToEntity();

        switch (user)
        {
            case Employee employee:
                await dbContext.Employees.AddAsync(employee, ct);
                break;
            case AlumniMember alumniMember:
                await dbContext.AlumniMembers.AddAsync(alumniMember, ct);
                break;
            case EmployeeMember employeeMember:
                await dbContext.EmployeeMembers.AddAsync(employeeMember, ct);
                break;
            case Member member:
                await dbContext.Members.AddAsync(member, ct);
                break;
            case ServiceAccount serviceAccount:
                await dbContext.ServiceAccounts.AddAsync(serviceAccount, ct);
                break;
            case SystemAdmin systemAdmin:
                await dbContext.SystemAdmins.AddAsync(systemAdmin, ct);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(user));
        }

        await dbContext.SaveChangesAsync(ct);
    }
}