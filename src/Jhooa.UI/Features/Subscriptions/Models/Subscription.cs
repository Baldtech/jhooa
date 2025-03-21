using Jhooa.UI.Features.Identity.Models;

namespace Jhooa.UI.Features.Subscriptions.Models;

public class Subscription
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required DateOnly Start { get; init; }
    public DateOnly? End { get; set; }
    public DateTime? PaidAt { get; set; }
    public SubscriptionType Type { get; init; }
    public SubscriptionStatus Status { get; private set; } = SubscriptionStatus.NotPaid;
    public required Guid UserId { get; init; }
    public ApplicationUser User { get; set; } = null!;

    public string? StripePaymentIntentId { get; set; }
    public string? StripeSubscriptionId { get; private set; }
    public string? StripeSessionCheckoutId { get; init; }
    public bool IsPaid => PaidAt.HasValue;

    public void MarkPaymentAsPaid(string stripePaymentIntentId)
    {
        if (Status == SubscriptionStatus.Paid)
        {
            return;
        }

        StripePaymentIntentId = stripePaymentIntentId;
        PaidAt = DateTime.Now;
        Status = SubscriptionStatus.Paid;
    }

    public void MarkSubscriptionAsPaid(string stripeSubscriptionId)
    {
        if (Status == SubscriptionStatus.Paid) return;
        

        StripeSubscriptionId = stripeSubscriptionId;
        PaidAt = DateTime.Now;
        Status = SubscriptionStatus.Paid;
    }
    
    public void MarkSubscriptionAsPaid()
    {
        if (Status == SubscriptionStatus.Paid) return;

        PaidAt = DateTime.Now;
        Status = SubscriptionStatus.Paid;
    }
    
    public void MarkSubscriptionAsCancelled()
    {
        if (Status != SubscriptionStatus.Paid) return;
        
        Status = SubscriptionStatus.Cancelled;
        End = DateOnly.FromDateTime(DateTime.Now);
    }
}