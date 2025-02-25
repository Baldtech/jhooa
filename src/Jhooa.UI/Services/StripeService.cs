using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Jhooa.UI.Features.Subscriptions.Models.DTO;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Jhooa.UI.Services;

public class StripeService(
    IHttpContextAccessor httpContextAccessor,
    IOptions<Jhooa.UI.Configuration.StripeConfiguration> stripeConfig) : IStripeService
{
    private readonly Jhooa.UI.Configuration.StripeConfiguration _stripeConfig = stripeConfig.Value;


    public async Task<PaymentIntentResponse> GenerateFirstPaymentIntent(string stripeCustomerId,
        SubscriptionType subscriptionType)
    {
        var domain =
            $"{httpContextAccessor.HttpContext?.Request.Scheme}://{httpContextAccessor.HttpContext?.Request.Host.Value}";

        var options = new SessionCreateOptions
        {
            Customer = stripeCustomerId,
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Price = GetPrice(subscriptionType),
                    Quantity = 1,
                },
            ],
            AllowPromotionCodes = true,
            Mode = GetMode(subscriptionType),
            SuccessUrl = domain + "/Account/RegisterConfirmation?session-id={CHECKOUT_SESSION_ID}",
            CancelUrl = domain + "?session-id={CHECKOUT_SESSION_ID}&cancel=true"
        };
        
        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return new PaymentIntentResponse()
        {
            SessionId = session.Id,
            SessionUrl = session.Url,
        };
    }
    
    public async Task<PaymentIntentResponse> GeneratePaymentIntent(string stripeCustomerId,
        SubscriptionType subscriptionType)
    {
        var domain =
            $"{httpContextAccessor.HttpContext?.Request.Scheme}://{httpContextAccessor.HttpContext?.Request.Host.Value}";

        var options = new SessionCreateOptions
        {
            Customer = stripeCustomerId,
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Price = GetPrice(subscriptionType),
                    Quantity = 1,
                },
            ],
            AllowPromotionCodes = true,
            Mode = GetMode(subscriptionType),
            SuccessUrl = domain + "/Account/Manage/Subscription?session-id={CHECKOUT_SESSION_ID}",
            CancelUrl = domain,
        };
        
        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return new PaymentIntentResponse()
        {
            SessionId = session.Id,
            SessionUrl = session.Url,
        };
    }

    private static string GetMode(SubscriptionType subscriptionType)
        => subscriptionType is SubscriptionType.YearlyRecurring or SubscriptionType.MonthlyRecurring
            ? "subscription"
            : "payment";

    private string GetPrice(SubscriptionType subscriptionType)
        => subscriptionType switch
        {
            SubscriptionType.MonthlyOnce => _stripeConfig.MonthlyOncePriceId,
            SubscriptionType.YearlyOnce => _stripeConfig.AnnualOncePriceId,
            SubscriptionType.MonthlyRecurring => _stripeConfig.MonthlyRecurringPriceId,
            SubscriptionType.YearlyRecurring => _stripeConfig.AnnualRecurringPriceId,
            _ => throw new ArgumentOutOfRangeException(nameof(subscriptionType), subscriptionType, null)
        };

    public async Task<string> EnsureCustomer(ApplicationUser user)
    {
        var service = new CustomerService();
        var result = await service.SearchAsync(new CustomerSearchOptions()
        {
            Query = $"email:'{user.Email}'",
        });

        if (result.Any())
        {
            return result.First().Id;
        }

        var options = new CustomerCreateOptions
        {
            Email = user.Email,
            Name =
                $"{user.LastName.ToUpper(CultureInfo.CurrentCulture)} {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}",
        };
        var customer = await service.CreateAsync(options);

        return customer.Id;
    }


    public async Task<CheckoutSessionCompletedResponse?> HandleCheckoutSessionCompleted(string sessionId)
    {
        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(sessionId);

        return string.Equals(session.PaymentStatus, "paid", StringComparison.OrdinalIgnoreCase)
            ? new CheckoutSessionCompletedResponse()
            {
                PaymentIntentId = session.PaymentIntentId,
                SubscriptionId = session.SubscriptionId,
            }
            : null;
    }

    public async Task<Result<string>> RetrieveSubscriptionId(string sessionId)
    {
        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(sessionId);

        if (string.Equals(session.PaymentStatus, "Paid", StringComparison.OrdinalIgnoreCase))
        {
            return session.SubscriptionId;
        }

        return Result.Fail("Payment not completed");
        
    }

    public async Task<Result<string>> CancelSubscription(string subscriptionId)
    {
        try
        {
            var subService = new SubscriptionService();
            var result = await subService.CancelAsync(subscriptionId);

            return result.Status;
        }
        catch (StripeException e) when (string.Equals(e.StripeError?.Code, "resource_missing", StringComparison.Ordinal))
        {
            return Result.Ok("Subscription not found but OK");
        }
        catch (Exception)
        {
            return Result.Fail("Subscription not cancelled");
        }
        
    }

    public async Task<Result<string>> RetrievePaymentIntentIdIfPaid(string sessionId)
    {
        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(sessionId);
        
        if (string.Equals(session.PaymentStatus, "Paid", StringComparison.OrdinalIgnoreCase))
        {
            return session.PaymentIntentId;
        }

        return Result.Fail("Payment not completed");
    }
}