using System.Diagnostics.CodeAnalysis;
using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

[Mapper]
public static partial class UserMapper
{
    #region gRPC mappings

    public static partial Protos.User.V1.User ToGrpc(this User user);
    public static partial IEnumerable<Protos.User.V1.User> ToGrpc(this IEnumerable<User> user);

    #endregion
}