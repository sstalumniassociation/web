using FastEndpoints;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.BatchCreate;

public class BatchCreateUserEndpoint(AppDbContext dbContext)
    : Endpoint<BatchCreateUserRequest, IEnumerable<UserResponse>>
{
    public override void Configure()
    {
        Post("/User:Batch");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(BatchCreateUserRequest req, CancellationToken ct)
    {
        var users = req.Users.Select(u => u.ToEntity());

        foreach (var user in users)
        {
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
        }

        await dbContext.SaveChangesAsync(ct);
    }
}