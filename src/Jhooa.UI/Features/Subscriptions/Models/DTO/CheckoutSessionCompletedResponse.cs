namespace Jhooa.UI.Features.Subscriptions.Models.DTO;

public class CheckoutSessionCompletedResponse
{
    public string? PaymentIntentId { get; init; }
    public string? SubscriptionId { get; init; }
}