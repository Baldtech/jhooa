namespace Jhooa.UI.Features.Subscriptions.Models;

public class Subscription
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required DateOnly Start { get; init; }
    public required DateOnly? End { get; init; }
    public DateTime? PaidAt { get;  set; }
    public SubscriptionType Type { get; init; }
    public required Guid UserId { get; init; }
    public string? StripePaymentIntentId { get;  set; }
    public string? StripeSubscriptionId { get;  set; }
    public required string StripeSessionCheckoutId { get; init; }
    public bool IsPaid => PaidAt.HasValue;
}