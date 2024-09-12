using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

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
    public static partial IEnumerable<Protos.User.V1.User> ToGrpc(this IEnumerable<User> user);

    #endregion
}