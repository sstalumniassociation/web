using SSTAlumniAssociation.Core.Dtos.User;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.CheckIn;

public abstract class CheckInResponse
{
    public Guid Id { get; set; }
    public DateTime CheckInDateTime { get; set; }
    public DateTime? CheckOutDateTime { get; set; }
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
    // TODO : restrict information available in this response
    public UserResponse User { get; set; }
}
