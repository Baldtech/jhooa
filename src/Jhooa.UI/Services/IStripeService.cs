using Jhooa.UI.Features.Subscriptions.Models;

namespace Jhooa.UI.Services;

public interface IStripeService
{
    Task<string> GeneratePaymentIntent(Guid userId, SubscriptionType subscriptionType);
    
    Task<bool> HandleCheckoutSessionCompleted(string sessionId);
}