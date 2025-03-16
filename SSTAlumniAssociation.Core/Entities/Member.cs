using Microsoft.EntityFrameworkCore;

namespace SSTAlumniAssociation.Core.Entities;

[Index(nameof(MemberId), IsUnique = true)]
public abstract class Member : User
{
    /// <summary>
    /// Internal member ID used for tracking by SSTAA admin.
    /// </summary>
    public required string MemberId { get; set; }

    public required string PreferredName { get; set; }
    public required string Phone { get; set; }
    public required string Telegram { get; set; }
    public required string MailingAddress { get; set; }
    public required DateOnly DateOfBirth { get; set; }

    public required string SstEmail { get; set; }

    /// <summary>
    /// This property will be null if the user does not have access to their SST account.
    /// The membership will need to be manually approved using <see cref="ManualMemberApproval"/>
    /// </summary>
    public string? SstFirebaseId { get; set; }

    public MembershipSubscription? ActiveSubscription => SstFirebaseId is null && ManualMemberApproval is null
        ? throw new Exception($"Member must have {SstFirebaseId} in order to subscribe.")
        : Subscriptions
            .SingleOrDefault(s =>
                s.StartDateTime <= DateTime.Now &&
                s.EndDateTime >= DateTime.Now &&
                s.PaymentIntentState == PaymentIntentState.Success
            );

    #region Navigations

    public ManualMemberApproval? ManualMemberApproval { get; set; }
    public ICollection<Group> Groups { get; } = [];
    public ICollection<MembershipSubscription> Subscriptions { get; } = [];

    #endregion
}