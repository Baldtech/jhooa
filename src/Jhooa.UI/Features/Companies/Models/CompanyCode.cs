using Jhooa.UI.Features.Subscriptions.Models;

namespace Jhooa.UI.Features.Companies.Models;

public class CompanyCode
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Code { get; init; }
    public required Guid CompanyId { get; init; }
    public required int Duration { get; init; }
    
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset? UsedAt { get; init; }
    
    public Guid? SubscriptionId { get; set; }
    
    public Company Company { get; set; } = null!;
    public Subscription? Subscription { get; set; } = null!;
}