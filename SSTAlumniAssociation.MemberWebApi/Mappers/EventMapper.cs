using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.MemberWebApi.Endpoints.Event;
using SSTAlumniAssociation.MemberWebApi.Endpoints.Event.Attendee;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(UserMapper))]
public static partial class EventMapper
{
    public static partial EventResponse ToResponse(this Event @event);
    public static partial IEnumerable<EventResponse> ToResponse(this IEnumerable<Event> events);
}