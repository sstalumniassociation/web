using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
public static partial class EventMapper
{
    #region gRPC mappings

    public static partial Protos.Event.V1.Event ToGrpc(this Event @event);
    public static partial IEnumerable<Protos.Event.V1.Event> ToGrpc(this IQueryable<Event> @event);
    
    public static partial IEnumerable<Protos.Event.V1.AttendeeSimple> ToGrpc(this IEnumerable<Attendee> @event);

    #endregion
    
    #region Entity mappings
    
    public static partial Event ToEntity(this Protos.Event.V1.Event @event);

    #endregion
}