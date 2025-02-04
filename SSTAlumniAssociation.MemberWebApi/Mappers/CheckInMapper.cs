using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
public static partial class CheckInMapper
{
    #region gRPC mappings

    public static partial IEnumerable<Protos.CheckIn.V1.CheckIn> ToGrpc(this ICollection<UserCheckIn> checkIn);

    #endregion
}