using Riok.Mapperly.Abstractions;

namespace SSTAlumniAssociation.WebApi.Mappers;

[Mapper]
public static partial class UserMapper
{
    public static partial Protos.User.V1.User ToGrpcUser(this Entities.User user);
    public static partial IEnumerable<Protos.User.V1.User> ToGrpcUsers(this IEnumerable<Entities.User> user);

    [MapperIgnoreTarget(nameof(Entities.User.Events))]
    [MapperIgnoreTarget(nameof(Entities.User.UserEvents))]
    public static partial Entities.User ToUser(this Protos.User.V1.User user);
    
    [MapperIgnoreTarget(nameof(Entities.AlumniMember.Events))]
    [MapperIgnoreTarget(nameof(Entities.AlumniMember.UserEvents))]
    [MapperIgnoreTarget(nameof(Entities.AlumniMember.Groups))]
    [MapNestedProperties(nameof(Protos.User.V1.User.Member))]
    [MapNestedProperties([
        nameof(Protos.User.V1.User.Member),
        nameof(Protos.User.V1.User.Member.AlumniMember)
    ])]
    public static partial Entities.AlumniMember ToAlumniMember(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(Entities.EmployeeMember.Events))]
    [MapperIgnoreTarget(nameof(Entities.EmployeeMember.UserEvents))]
    [MapperIgnoreTarget(nameof(Entities.EmployeeMember.Groups))]
    [MapNestedProperties(nameof(Protos.User.V1.User.Member))]
    [MapNestedProperties([
        nameof(Protos.User.V1.User.Member),
        nameof(Protos.User.V1.User.Member.EmployeeMember)
    ])]
    public static partial Entities.EmployeeMember ToEmployeeMember(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(Entities.ServiceAccount.Events))]
    [MapperIgnoreTarget(nameof(Entities.ServiceAccount.UserEvents))]
    [MapNestedProperties(nameof(Protos.User.V1.User.ServiceAccount))]
    public static partial Entities.ServiceAccount ToServiceAccount(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(Entities.SystemAdmin.Events))]
    [MapperIgnoreTarget(nameof(Entities.SystemAdmin.UserEvents))]
    [MapNestedProperties(nameof(Protos.User.V1.User.SystemAdmin))]
    public static partial Entities.SystemAdmin ToSystemAdmin(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(Entities.Employee.Events))]
    [MapperIgnoreTarget(nameof(Entities.Employee.UserEvents))]
    [MapNestedProperties(nameof(Protos.User.V1.User.Employee))]
    public static partial Entities.Employee ToEmployee(this Protos.User.V1.User user);

    [MapEnum(EnumMappingStrategy.ByName)]
    private static partial Entities.Membership ToMembership(this Protos.User.V1.Membership membership);

    [MapEnum(EnumMappingStrategy.ByName)]
    private static partial Entities.ServiceAccountType ToServiceAccountType(
        this Protos.User.V1.ServiceAccountType serviceAccountType);
}