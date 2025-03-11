namespace SSTAlumniAssociation.Core.Dtos.User;

public abstract class MemberResponse : UserResponse
{
    public string MemberId { get; set; }
    public MembershipSubscriptionResponse? ActiveSubscription { get; set; }
}