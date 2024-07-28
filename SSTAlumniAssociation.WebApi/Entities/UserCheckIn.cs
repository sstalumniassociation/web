namespace SSTAlumniAssociation.WebApi.Entities;

public class UserCheckIn : CheckIn
{
    public User User { get; set; }
}