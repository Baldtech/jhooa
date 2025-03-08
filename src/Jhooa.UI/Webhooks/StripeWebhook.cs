using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace Jhooa.UI.Webhooks;

[Route("webhooks/stripe")]
public class StripeWebhook(
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
                case EventTypes.ChargeSucceeded:
                {
                    var charge = stripeEvent.Data.Object as Charge;
                    
                    
                    
                    
                    logger.LogInformation("[Stripe webhook ({StripeEventId} - {EventType})] Charge succeeded: {ChargeId}",
                        stripeEvent.Id, stripeEvent.Type, charge?.Id);
                    break;
                }
                case EventTypes.PaymentIntentSucceeded:
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    
                    logger.LogInformation(
                        "[Stripe webhook ({StripeEventId} - {EventType})] Payment intent succeeded: {PaymentIntentId}",
                        stripeEvent.Id, stripeEvent.Type, paymentIntent?.Id);
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