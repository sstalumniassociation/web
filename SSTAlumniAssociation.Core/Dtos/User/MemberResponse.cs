namespace SSTAlumniAssociation.Core.Dtos.User;

public abstract class MemberResponse : UserResponse
{
    public string MemberId { get; set; }
    public string PreferredName { get; set; }
    public string Phone { get; set; }
    public string Telegram { get; set; }
    public string MailingAddress { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string SstFirebaseId { get; set; }
    public string SstEmail { get; set; }
    
    public MembershipSubscriptionResponse? ActiveSubscription { get; set; }
}