using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

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