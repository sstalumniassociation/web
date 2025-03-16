using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.MemberWebApi.Endpoints.Event;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Attendee.Get;

public class AttendeeResponse
{
    public Guid Id { get; set; }
    public Guid AdmissionKey { get; set; }
    public DateTime? AdmittedAt { get; set; }

    public EventResponse Event { get; set; }
    public UserResponse AdmittedBy { get; set; }
}