using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.User;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

[Mapper]
public static partial class UserMapper
{
    [MapDerivedType<EmployeeMember, EmployeeMemberResponse>]
    [MapDerivedType<AlumniMember, AlumniMemberResponse>]
    [MapDerivedType<Employee, EmployeeResponse>]
    [MapDerivedType<SystemAdmin, SystemAdminResponse>]
    [MapDerivedType<ServiceAccount, ServiceAccountResponse>]
    public static partial UserResponse ToResponse(this User user);
}