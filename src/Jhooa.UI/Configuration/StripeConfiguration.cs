namespace Jhooa.UI.Configuration;

public class StripeConfiguration
{
    public const string SectionName = "Stripe";
    
    public string ApiKey { get; init; } = "";
    public string ProductId { get; init; } = "";
    public string MonthlyOncePriceId { get; init; } = "";
    public string AnnualOncePriceId { get; init; } = "";
    public string MonthlyRecurringPriceId { get; init; } = "";
    public string AnnualRecurringPriceId { get; init; } = "";
}