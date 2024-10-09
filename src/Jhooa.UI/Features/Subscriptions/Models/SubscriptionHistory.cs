namespace Jhooa.UI.Features.Subscriptions.Models;

public class SubscriptionHistory
{
    public const int StripeCheckoutSessionIdMaxLength = 100;

    
    public Guid Id { get; init; } = Guid.NewGuid();
    public required SubscriptionType Type { get; init; }
    public DateTime BoughtAt { get; init; } = DateTime.Now;
    public required string StripeCheckoutSessionId { get; init; }
    public PaymentStatus PaymentStatus { get; init; } = PaymentStatus.NotPaid;
    public required Guid UserId { get; init; }
    
    public string TypeString => Type switch 
    {
        SubscriptionType.MonthlyOnce => "1 month",
        SubscriptionType.AnnualOnce => "1 year",
        SubscriptionType.MonthlyRecurring => "Monthly",
        SubscriptionType.AnnualRecurring => "Annual",
        _ => throw new ArgumentOutOfRangeException(paramName: nameof(Type)),};
}