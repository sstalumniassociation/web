namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee.BatchCreate;

public class BatchCreateEventAttendeeRequest
{
    public Guid Id { get; set; }
    public IEnumerable<Guid> UserIds { get; set; }
}