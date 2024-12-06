using Jhooa.UI.Features.Subscriptions.Models;

namespace Jhooa.UI.Services;

public interface IDreamService
{
    List<DateOnly> GetEligibleDates(Subscription subscription);
}