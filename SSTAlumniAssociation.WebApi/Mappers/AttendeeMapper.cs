using Riok.Mapperly.Abstractions;

namespace SSTAlumniAssociation.WebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
[UseStaticMapper(typeof(EventMapper))]
public static partial class AttendeeMapper
{
    #region gRPC mappings

    public static partial Protos.Attendee.V1.Attendee ToGrpc(this Entities.Attendee attendee);

    #endregion
}