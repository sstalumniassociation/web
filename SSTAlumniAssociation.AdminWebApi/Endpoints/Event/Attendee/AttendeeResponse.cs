using SSTAlumniAssociation.Core.Dtos.User;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee;

public class AttendeeResponse
{
    public Guid Id { get; set; }
    public string AdmissionKey { get; set; }
    public UserResponse User { get; set; }
}