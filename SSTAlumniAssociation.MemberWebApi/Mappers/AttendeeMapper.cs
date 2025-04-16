using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.MemberWebApi.Endpoints.Attendee.Get;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(UserMapper))]
public static partial class AttendeeMappper
{
    public static partial AttendeeResponse ToResponse(this Attendee attendee);
    public static partial IEnumerable<AttendeeResponse> ToResponse(this IEnumerable<Attendee> attendees);
}