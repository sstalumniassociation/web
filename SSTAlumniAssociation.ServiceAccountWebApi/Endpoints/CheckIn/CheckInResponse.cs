using FastEndpoints;
using SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.User;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.CheckIn;

public abstract class CheckInResponse
{
    public Guid Id { get; set; }
    public DateTime CheckInDateTime { get; set; }
    public DateTime CheckOutDateTime { get; set; }
}

public class GuestCheckInResponse : CheckInResponse
{
    public string Name { get; set; }
    public string Nric { get; set; }
    public string Phone { get; set; }
    public string Reason { get; set; }
}

public class UserCheckInResponse : CheckInResponse
{
    public UserResponse User { get; set; }
}
