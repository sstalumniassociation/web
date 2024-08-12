namespace SSTAlumniAssociation.WebApi.Entities;

public class MembershipSubscriptionPayment
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// Payment processor
    /// </summary>
    public required PaymentProcessor Processor { get; set; }

    /// <summary>
    /// Payment processor's payment intent / request ID
    /// </summary>
    public required string IntentId { get; set; }

    /// <summary>
    /// Payment processor's payment intent / request state
    /// </summary>
    public required string IntentState { get; set; }

    #region Navigations

    public MembershipSubscription MembershipSubscription { get; set; }

    #endregion
}