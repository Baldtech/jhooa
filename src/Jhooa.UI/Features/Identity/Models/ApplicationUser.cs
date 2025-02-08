using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Events.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Microsoft.AspNetCore.Identity;

namespace Jhooa.UI.Features.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? StripeCustomerId { get; init; }
    
    public required string Address { get; init; }
    public required string Country { get; init; }
    public required string ZipCode { get; init; }
    public required string City { get; init; }
    public required DateTimeOffset BirthDate { get; init; }
    
    public required bool NewsletterActive { get; init; }
    public required bool AcceptTos { get; init; }
    public required bool AcceptDistribution { get; init; }
    public required DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset? ActivatedAt { get; init; }

    public List<Registration> Registrations { get; init; } = [];
    public List<Subscription> Subscriptions { get; init; } = [];
    public List<Dream> Dreams { get; init; } = [];
    
    
    public char SubType => Subscriptions.Exists(s => s.Start <= DateOnly.FromDateTime(DateTime.Now) && (s.End >= DateOnly.FromDateTime(DateTime.Now) || s.End is null)) 
        ? (Subscriptions.First(s => s.Start <= DateOnly.FromDateTime(DateTime.Now) && (s.End >= DateOnly.FromDateTime(DateTime.Now) || s.End is null)).Type is SubscriptionType.MonthlyOnce || Subscriptions.First(s => s.Start <= DateOnly.FromDateTime(DateTime.Now) && (s.End >= DateOnly.FromDateTime(DateTime.Now) || s.End is null)).Type is SubscriptionType.MonthlyRecurring ? 'M' : 'A')
        : '-';
}