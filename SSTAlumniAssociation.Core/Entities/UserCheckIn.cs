namespace SSTAlumniAssociation.Core.Entities;

public class UserCheckIn : CheckIn
{
    public Guid UserId { get; set; }
    public User User { get; set; }
}