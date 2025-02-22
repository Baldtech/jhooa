using Jhooa.UI.Features.Subscriptions.Models;

namespace Jhooa.UI.Features.Identity.Models;

public class UserView
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public bool IsSubscriptionActive { get; set; }
    public SubscriptionType? SubscriptionType { get; set; }
    public SubscriptionStatus? SubscriptionStatus { get; set; }
    public DateOnly? SubscriptionStartDate { get; init; }
    public DateOnly? SubscriptionEndDate { get; set; }
    
    public char SubType => IsSubscriptionActive 
        ? SubscriptionType is Jhooa.UI.Features.Subscriptions.Models.SubscriptionType.MonthlyOnce or Jhooa.UI.Features.Subscriptions.Models.SubscriptionType.MonthlyRecurring ? 'M' : 'A'
        : '-';
}