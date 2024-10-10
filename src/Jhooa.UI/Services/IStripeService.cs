using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Jhooa.UI.Features.Subscriptions.Models.DTO;

namespace Jhooa.UI.Services;

public interface IStripeService
{
    Task<PaymentIntentResponse> GeneratePaymentIntent(string stripeCustomerId, SubscriptionType subscriptionType);
    
    /// <summary>
    ///     Returns payment intent id
    /// </summary>
    /// <param name="sessionId"></param>
    /// <returns></returns>
    Task<CheckoutSessionCompletedResponse?> HandleCheckoutSessionCompleted(string sessionId);

    Task<string> RetrieveSubscriptionId(string sessionId);
    Task<string> RetrievePaymentIntentId(string sessionId);
    
    Task<string> EnsureCustomer(ApplicationUser user);
}