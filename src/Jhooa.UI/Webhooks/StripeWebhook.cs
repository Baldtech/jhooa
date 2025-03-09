using Jhooa.UI.Data;
using Jhooa.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Jhooa.UI.Webhooks;

[Route("webhooks/stripe")]
public class StripeWebhook(
    IStripeService stripeService,
    ApplicationDbContext dbContext,
    ILogger<StripeWebhook> logger,
    IOptions<Jhooa.UI.Configuration.StripeConfiguration> stripeConfig) : Controller
{
    [HttpPost]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var signatureHeader = Request.Headers["Stripe-Signature"];

            var stripeEvent = EventUtility.ConstructEvent(json,
                signatureHeader, stripeConfig.Value.WebhookSecret);

            switch (stripeEvent.Type)
            {
                case EventTypes.InvoicePaymentFailed:
                {
                    var invoice = stripeEvent.Data.Object as Invoice;
                    logger.LogWarning(
                        "[Stripe webhook ({StripeEventId} - {EventType})] Payment failed for subscription: {SubId}",
                        stripeEvent.Id, stripeEvent.Type, invoice?.SubscriptionId);
                    break;
                }
                case EventTypes.CustomerSubscriptionUpdated:
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    logger.LogInformation("[Stripe webhook ({StripeEventId} - {EventType})] Subscription updated: {SubId}",
                        stripeEvent.Id, stripeEvent.Type, subscription?.Id);
                    break;
                }
                case EventTypes.CheckoutSessionCompleted:
                {
                    var session = stripeEvent.Data.Object as Session;
                    logger.LogInformation("[Stripe webhook ({StripeEventId} - {EventType})] Checkout session completed: {SessionId}",
                        stripeEvent.Id, stripeEvent.Type, session?.Id);

                    if (session is null)
                    {
                        logger.LogWarning("[Stripe webhook ({StripeEventId} - {EventType})] Unable to parse the session: {SessionId}",
                            stripeEvent.Id, stripeEvent.Type, session?.Id);
                        break;
                    }
                    
                    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.StripeCustomerId == session.CustomerId);

                    if (user is null)
                    {
                        logger.LogWarning("[Stripe webhook ({StripeEventId} - {EventType})] Unable to find user with stripe customer id: {CustomerId}",
                            stripeEvent.Id, stripeEvent.Type, session.CustomerId);
                        break;
                    }
                    
                    // Subscription
                    if (session.SubscriptionId != null)
                    {
                        var subscriptionId = await stripeService.RetrieveSubscriptionId(session.Id);
                        logger.LogInformation("Subscription id: {SubscriptionId}", subscriptionId);
                        
                        var sub = await dbContext.Subscriptions.FirstOrDefaultAsync(s => s.StripeSessionCheckoutId == session.Id);
                        if (sub is null)
                        {
                            logger.LogWarning("[Stripe webhook ({StripeEventId} - {EventType})] Unable to find subscription with stripe session id: {SessionId}",
                                stripeEvent.Id, stripeEvent.Type, session.Id);
                            break;
                        }
                        
                        if (string.Equals(session.PaymentStatus, "Paid", StringComparison.OrdinalIgnoreCase))
                        {
                            sub.MarkSubscriptionAsPaid(subscriptionId.Value);
                            
                            if (user.ActivatedAt is null)
                            {
                                await dbContext.Users.Where(e => e.Id == user.Id)
                                    .ExecuteUpdateAsync(setters => setters
                                        .SetProperty(b => b.ActivatedAt, DateTimeOffset.Now)
                                        .SetProperty(b => b.EmailConfirmed, true));
                            }
                        }
                    }
                    // Payment intent
                    else if (session.PaymentIntentId != null)
                    {
                        var paymentIntentId = await stripeService.RetrievePaymentIntentIdIfPaid(session.Id);
                        logger.LogInformation("Payment intent id: {PaymentIntentId}", paymentIntentId);
                        
                        var sub = await dbContext.Subscriptions.FirstOrDefaultAsync(s => s.StripeSessionCheckoutId == session.Id);
                        if (sub is null)
                        {
                            logger.LogWarning("[Stripe webhook ({StripeEventId} - {EventType})] Unable to find subscription with stripe session id: {SessionId}",
                                stripeEvent.Id, stripeEvent.Type, session.Id);
                            break;
                        }
                        
                        if (string.Equals(session.PaymentStatus, "Paid", StringComparison.OrdinalIgnoreCase))
                        {
                            sub.MarkPaymentAsPaid(paymentIntentId.Value);
                            
                            if (user.ActivatedAt is null)
                            {
                                await dbContext.Users.Where(e => e.Id == user.Id)
                                    .ExecuteUpdateAsync(setters => setters
                                        .SetProperty(b => b.ActivatedAt, DateTimeOffset.Now)
                                        .SetProperty(b => b.EmailConfirmed, true));
                                
                            }
                        }
                    }

                    await dbContext.SaveChangesAsync();
                    
                    break;
                }
                default:

                    logger.LogInformation("[Stripe webhook ({StripeEventId} - {EventType})] Unhandled event type",
                        stripeEvent.Id, stripeEvent.Type);
                    break;
            }

            return Ok();
        }
        catch (StripeException e)
        {
            logger.LogError(e, "Error: {Msg}", e.Message);
            return BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}