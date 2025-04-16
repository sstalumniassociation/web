namespace SSTAlumniAssociation.MemberWebApi.Endpoints.CheckIn;

public class CheckInResponse
{
    public Guid Id { get; set; }
    public DateTime CheckInDateTime { get; set; }
    public DateTime? CheckOutDateTime { get; set; }
}
