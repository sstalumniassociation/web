using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.AdminWebApi.Endpoints.User;
using SSTAlumniAssociation.AdminWebApi.Endpoints.User.Create;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Mappers;

[Mapper]
public static partial class UserMapper
{
    [MapDerivedType<EmployeeMember, EmployeeMemberResponse>]
    [MapDerivedType<AlumniMember, AlumniMemberResponse>]
    [MapDerivedType<Employee, EmployeeResponse>]
    [MapDerivedType<SystemAdmin, SystemAdminResponse>]
    [MapDerivedType<ServiceAccount, ServiceAccountResponse>]
    public static partial UserResponse ToResponse(this User user);
    
    [MapDerivedType<CreateEmployeeMemberRequest, EmployeeMember>]
    [MapDerivedType<CreateAlumniMemberRequest, AlumniMember>]
    [MapDerivedType<CreateEmployeeRequest, Employee>]
    [MapDerivedType<CreateSystemAdminRequest, SystemAdmin>]
    [MapDerivedType<CreateServiceAccountRequest, ServiceAccount>]
    public static partial User ToEntity(this CreateUserRequest req);
}