using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.WebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
public static partial class EventMapper
{
    #region gRPC mappings

    public static partial Protos.Event.V1.EventSimple ToGrpcSimpleEvent(this Event @event);
    public static partial Protos.Event.V1.Event ToGrpcEvent(this Event @event);

    public static partial IEnumerable<Protos.Event.V1.EventSimple> ToGrpcSimpleEvent(
        this IQueryable<Event> @event
        );

    #endregion

    #region Entity mappings

    [MapperIgnoreSource(nameof(Protos.Event.V1.Event.Attendees))]
    public static partial Event ToEntity(this Protos.Event.V1.Event @event);
    
    public static partial Event ToEntity(this Protos.Event.V1.EventSimple @event);

    #endregion
}