using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
public static partial class EventMapper
{
    #region gRPC mappings

    public static partial Protos.Event.V1.Event ToGrpc(this Event @event);
    public static partial IEnumerable<Protos.Event.V1.Event> ToGrpc(this IQueryable<Event> @event);
    
    public static partial Protos.Event.V1.Attendee ToGrpc(this Attendee attendee);

    #endregion
}