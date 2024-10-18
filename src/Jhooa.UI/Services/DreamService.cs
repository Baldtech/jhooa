using Jhooa.UI.Features.Subscriptions.Models;

namespace Jhooa.UI.Services;

public class DreamService : IDreamService
{
    public List<DateOnly> GetEligibleDates(Subscription subscription)
    {
        var eligibleMonths = new List<DateOnly>();

        var currentDate = new DateOnly(subscription.Start.Year, subscription.Start.Month, 1);

        if (subscription.End is not null)
        {
            while (currentDate <= subscription.End.Value.AddMonths(-1))
            {
                eligibleMonths.Add(currentDate);
                currentDate = currentDate.AddMonths(1);
            }
        }
        else
        {
            while (currentDate <= subscription.Start.AddMonths(1))
            {
                eligibleMonths.Add(currentDate);
                currentDate = currentDate.AddMonths(1);
            }
        }

        return eligibleMonths;
    }
}