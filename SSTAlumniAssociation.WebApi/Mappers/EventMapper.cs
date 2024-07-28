using Riok.Mapperly.Abstractions;

namespace SSTAlumniAssociation.WebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
public static partial class EventMapper
{
    #region gRPC mappings

    public static partial Protos.Event.V1.EventSimple ToGrpcSimpleEvent(this Entities.Event @event);
    public static partial Protos.Event.V1.Event ToGrpcEvent(this Entities.Event @event);

    public static partial IEnumerable<Protos.Event.V1.EventSimple> ToGrpcSimpleEvent(
        this IQueryable<Entities.Event> @event
        );

    #endregion

    #region Entity mappings

    [MapperIgnoreSource(nameof(Protos.Event.V1.Event.Attendees))]
    public static partial Entities.Event ToEntity(this Protos.Event.V1.Event @event);
    
    public static partial Entities.Event ToEntity(this Protos.Event.V1.EventSimple @event);

    #endregion
}