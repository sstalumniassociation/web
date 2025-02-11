using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.AdminWebApi.Endpoints.Event;
using SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(UserMapper))]
public static partial class EventMapper
{
    public static partial EventResponse ToResponse(this Event @event);
    public static partial IEnumerable<EventResponse> ToResponse(this IEnumerable<Event> events);

    public static partial AttendeeResponse ToResponse(this Attendee attendee);
    public static partial IEnumerable<AttendeeResponse> ToResponse(this IEnumerable<Attendee> attendees);
}