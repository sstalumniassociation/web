using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

[Mapper]
public static partial class UserMapper
{
    #region gRPC mappings

    [MapDerivedType<Employee, Protos.User.V1.User>]
    [MapDerivedType<EmployeeMember, Protos.User.V1.User>]
    [MapDerivedType<AlumniMember, Protos.User.V1.User>]
    [MapDerivedType<ServiceAccount, Protos.User.V1.User>]
    [MapDerivedType<SystemAdmin, Protos.User.V1.User>]
    public static partial Protos.User.V1.User ToGrpc(this User user);

    private static partial Protos.User.V1.User ToGrpcBase(this User user);
    private static partial Protos.User.V1.Member ToGrpcMemberBase(this Member user);
    private static partial Protos.User.V1.Employee ToGrpcEmployeeBase(this Employee user);
    private static partial Protos.User.V1.EmployeeMember ToGrpcEmployeeMemberBase(this EmployeeMember user);
    private static partial Protos.User.V1.AlumniMember ToGrpcAlumniMemberBase(this AlumniMember user);
    private static partial Protos.User.V1.ServiceAccount ToGrpcServiceAccountBase(this ServiceAccount user);
    private static partial Protos.User.V1.SystemAdmin ToGrpcSystemAdminBase(this SystemAdmin user);

    public static partial IEnumerable<Protos.User.V1.User> ToGrpc(this IEnumerable<User> user);

    [MapEnum(EnumMappingStrategy.ByName)]
    private static partial Protos.User.V1.ServiceAccountType
        ToGrpc(this ServiceAccountType serviceAccountType);

    private static Protos.User.V1.User EmployeeToGrpcUser(Employee user)
    {
        var u = user.ToGrpcBase();
        u.Employee = user.ToGrpcEmployeeBase();
        return u;
    }

    private static Protos.User.V1.User EmployeeMemberToGrpcUser(EmployeeMember user)
    {
        var u = user.ToGrpcBase();
        u.Member = user.ToGrpcMemberBase();
        u.Member.EmployeeMember = user.ToGrpcEmployeeMemberBase();
        return u;
    }

    private static Protos.User.V1.User AlumniMemberToGrpcUser(AlumniMember user)
    {
        var u = user.ToGrpcBase();
        u.Member = user.ToGrpcMemberBase();
        u.Member.AlumniMember = user.ToGrpcAlumniMemberBase();
        return u;
    }

    private static Protos.User.V1.User ServiceAccountToGrpcUser(ServiceAccount user)
    {
        var u = user.ToGrpcBase();
        u.ServiceAccount = user.ToGrpcServiceAccountBase();
        return u;
    }

    private static Protos.User.V1.User SystemAdminToGrpcUser(SystemAdmin user)
    {
        var u = user.ToGrpcBase();
        u.SystemAdmin = user.ToGrpcSystemAdminBase();
        return u;
    }

    #endregion

    #region Entity mappings

    [MapperIgnoreTarget(nameof(User.Events))]
    [MapperIgnoreTarget(nameof(User.UserEvents))]
    public static partial User ToUser(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(AlumniMember.Events))]
    [MapperIgnoreTarget(nameof(AlumniMember.UserEvents))]
    [MapperIgnoreTarget(nameof(AlumniMember.Groups))]
    [MapNestedProperties(nameof(Protos.User.V1.User.Member))]
    [MapNestedProperties([
        nameof(Protos.User.V1.User.Member),
        nameof(Protos.User.V1.User.Member.AlumniMember)
    ])]
    public static partial AlumniMember ToAlumniMember(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(EmployeeMember.Events))]
    [MapperIgnoreTarget(nameof(EmployeeMember.UserEvents))]
    [MapperIgnoreTarget(nameof(EmployeeMember.Groups))]
    [MapNestedProperties(nameof(Protos.User.V1.User.Member))]
    [MapNestedProperties([
        nameof(Protos.User.V1.User.Member),
        nameof(Protos.User.V1.User.Member.EmployeeMember)
    ])]
    public static partial EmployeeMember ToEmployeeMember(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(ServiceAccount.Events))]
    [MapperIgnoreTarget(nameof(ServiceAccount.UserEvents))]
    [MapNestedProperties(nameof(Protos.User.V1.User.ServiceAccount))]
    public static partial ServiceAccount ToServiceAccount(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(SystemAdmin.Events))]
    [MapperIgnoreTarget(nameof(SystemAdmin.UserEvents))]
    [MapNestedProperties(nameof(Protos.User.V1.User.SystemAdmin))]
    public static partial SystemAdmin ToSystemAdmin(this Protos.User.V1.User user);

    [MapperIgnoreTarget(nameof(Employee.Events))]
    [MapperIgnoreTarget(nameof(Employee.UserEvents))]
    [MapNestedProperties(nameof(Protos.User.V1.User.Employee))]
    public static partial Employee ToEmployee(this Protos.User.V1.User user);

    [MapEnum(EnumMappingStrategy.ByName)]
    private static partial ServiceAccountType ToServiceAccountType(
        this Protos.User.V1.ServiceAccountType serviceAccountType);

    #endregion
}