using Jhooa.UI.Data;
using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace Jhooa.UI.Services;

public class StripeService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext) : IStripeService
{
    public async Task<string> GeneratePaymentIntent(Guid userId, SubscriptionType subscriptionType)
    {
        var domain = $"{httpContextAccessor.HttpContext?.Request.Scheme}://{httpContextAccessor.HttpContext?.Request.Host.Value}";
        var user = dbContext.Users.First(u => u.Id == userId);
        
        var options = 
            subscriptionType switch
            {
                SubscriptionType.MonthlyOnce => GenerateOnceMonthlySession(domain, user),
                SubscriptionType.AnnualOnce => GenerateOnceAnnuallySession(domain, user),
                _ => throw new ArgumentOutOfRangeException(nameof(subscriptionType), subscriptionType, null),
            };

        var service = new SessionService();

        var session = await service.CreateAsync(options);

        await dbContext.SubscriptionHistories.AddAsync(new SubscriptionHistory()
        {
            StripeCheckoutSessionId = session.Id,
            Type = subscriptionType,
            UserId = userId,
        });
        await dbContext.SaveChangesAsync();
        
        return session.Url;
    }

    private static SessionCreateOptions GenerateOnceMonthlySession(string domain, ApplicationUser user)
    {
        var lineItems = new [] { new SessionLineItemOptions
        {
            Quantity = 1,
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "eur",
                UnitAmountDecimal = 5 * 100,
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "1 month",
                },
            },
        },};

        var options = new SessionCreateOptions
        {
            LineItems = lineItems.ToList(),
            CustomerEmail = user.Email,
            Mode = "payment",
            SuccessUrl = domain + "/Account/Manage/Subscription?session-id={CHECKOUT_SESSION_ID}",
            CancelUrl = domain,
        };
        return options;
    }
    
    private static SessionCreateOptions GenerateOnceAnnuallySession(string domain, ApplicationUser user)
    {
        var lineItems = new [] { new SessionLineItemOptions
        {
            Quantity = 1,
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "eur",
                UnitAmountDecimal = 50 * 100,
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "1 year",
                },
            },
        },};

        var options = new SessionCreateOptions
        {
            LineItems = lineItems.ToList(),
            CustomerEmail = user.Email,
            Mode = "payment",
            SuccessUrl = domain + "/Account/Manage/Subscription?session-id={CHECKOUT_SESSION_ID}",
            CancelUrl = domain,
        };
        return options;
    }

    public async Task<bool> HandleCheckoutSessionCompleted(string sessionId)
    {
        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(sessionId);
        
        if (string.Equals(session.PaymentStatus, "paid", StringComparison.OrdinalIgnoreCase))
        {
            await dbContext.SubscriptionHistories.Where(x => x.StripeCheckoutSessionId == sessionId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(b => b.PaymentStatus, PaymentStatus.Paid));
            await dbContext.SaveChangesAsync();

            return true;
        }

        return false;
    }
}