namespace SSTAlumniAssociation.Core.Dtos.User;

public class MembershipSubscriptionResponse
{
    public Guid Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string? PaymentIntentId { get; set; }
    public PaymentIntentStateResponse PaymentIntentState { get; set; } = PaymentIntentStateResponse.None;
    public MembershipPlanResponse Plan { get; set; }
}