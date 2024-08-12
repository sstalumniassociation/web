using Microsoft.EntityFrameworkCore;

namespace SSTAlumniAssociation.WebApi.Entities;

[Index(nameof(MemberId), IsUnique = true)]
public abstract class Member : User
{
    /// <summary>
    /// Internal member ID used for tracking by SSTAA admin.
    /// </summary>
    public required string MemberId { get; set; }

    public MembershipSubscription? ActiveSubscription => Subscriptions.SingleOrDefault(s =>
        s.StartDateTime <= DateTime.Now && s.EndDateTime >= DateTime.Now && s.PaymentIntentState == "success");

    #region Navigations

    public ICollection<Group> Groups { get; } = [];
    public ICollection<MembershipSubscription> Subscriptions { get; } = [];
    
    #endregion
}