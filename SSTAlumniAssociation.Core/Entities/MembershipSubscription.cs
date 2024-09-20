namespace SSTAlumniAssociation.Core.Entities;

public class MembershipSubscription
{
    public Guid Id { get; set; }

    /// <summary>
    /// Date and time that the subscription starts
    /// </summary>
    public required DateTime StartDateTime { get; set; }

    /// <summary>
    /// Date and time that the subscription ends
    /// </summary>
    public required DateTime EndDateTime { get; set; }

    /// <summary>
    /// Payment processor's payment intent / request ID
    /// </summary>
    public string? PaymentIntentId { get; set; }
    
    /// <summary>
    /// Payment processor's payment intent / request state
    /// </summary>
    public PaymentIntentState PaymentIntentState { get; set; } = PaymentIntentState.None;

    #region Navigations

    /// <summary>
    /// Member ID associated with the subscription
    /// </summary>
    public Guid MemberId { get; set; }

    /// <summary>
    /// Mmeber associated with the subscription
    /// </summary>
    public Member Member { get; set; }

    /// <summary>
    /// Membership plan ID associated with the subscription
    /// </summary>
    public Guid MembershipPlanId { get; set; }

    /// <summary>
    /// Membership plan associated with the subscription
    /// </summary>
    public MembershipPlan MembershipPlan { get; set; }

    public ICollection<MembershipSubscriptionPayment> Payments { get; } = [];

    #endregion
}