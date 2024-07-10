using Riok.Mapperly.Abstractions;

namespace SSTAlumniAssociation.WebApi.Mappers;

[Mapper]
public static partial class UserMapper
{
    #region gRPC mappings

    [MapDerivedType<Entities.Employee, Protos.User.V1.User>]
    [MapDerivedType<Entities.EmployeeMember, Protos.User.V1.User>]
    [MapDerivedType<Entities.AlumniMember, Protos.User.V1.User>]
    [MapDerivedType<Entities.ServiceAccount, Protos.User.V1.User>]
    [MapDerivedType<Entities.SystemAdmin, Protos.User.V1.User>]
    public static partial Protos.User.V1.User ToGrpc(this Entities.User user);

    private static partial Protos.User.V1.User ToGrpcBase(this Entities.User user);
    private static partial Protos.User.V1.Member ToGrpcMemberBase(this Entities.Member user);
    private static partial Protos.User.V1.Employee ToGrpcEmployeeBase(this Entities.Employee user);
    private static partial Protos.User.V1.EmployeeMember ToGrpcEmployeeMemberBase(this Entities.EmployeeMember user);
    private static partial Protos.User.V1.AlumniMember ToGrpcAlumniMemberBase(this Entities.AlumniMember user);
    private static partial Protos.User.V1.ServiceAccount ToGrpcServiceAccountBase(this Entities.ServiceAccount user);
    private static partial Protos.User.V1.SystemAdmin ToGrpcSystemAdminBase(this Entities.SystemAdmin user);

    public static partial IEnumerable<Protos.User.V1.User> ToGrpc(this IEnumerable<Entities.User> user);

    [MapEnum(EnumMappingStrategy.ByName)]
    private static partial Protos.User.V1.ServiceAccountType
        ToGrpc(this Entities.ServiceAccountType serviceAccountType);

    private static Protos.User.V1.User EmployeeToGrpcUser(Entities.Employee user)
    {
        var u = user.ToGrpcBase();
        u.Employee = user.ToGrpcEmployeeBase();
        return u;
    }

    private static Protos.User.V1.User EmployeeMemberToGrpcUser(Entities.EmployeeMember user)
    {
        var u = user.ToGrpcBase();
        u.Member = user.ToGrpcMemberBase();
        u.Member.EmployeeMember = user.ToGrpcEmployeeMemberBase();
        return u;
    }

    private static Protos.User.V1.User AlumniMemberToGrpcUser(Entities.AlumniMember user)
    {
        var u = user.ToGrpcBase();
        u.Member = user.ToGrpcMemberBase();
        u.Member.AlumniMember = user.ToGrpcAlumniMemberBase();
        return u;
    }

    private static Protos.User.V1.User ServiceAccountToGrpcUser(Entities.ServiceAccount user)
    {
        var u = user.ToGrpcBase();
        u.ServiceAccount = user.ToGrpcServiceAccountBase();
        return u;
    }

    private static Protos.User.V1.User SystemAdminToGrpcUser(Entities.SystemAdmin user)
    {
        var u = user.ToGrpcBase();
        u.SystemAdmin = user.ToGrpcSystemAdminBase();
        return u;
    }

    #endregion

    #region Entity mappings

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

    #endregion
}