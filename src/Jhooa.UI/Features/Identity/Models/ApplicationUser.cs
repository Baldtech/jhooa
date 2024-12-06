using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Events.Models;
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
    
    public required bool NewsletterActive { get; init; }
    public required bool AcceptTos { get; init; }
    public required DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset? ActivatedAt { get; init; }

    public List<Registration> Registrations { get; init; } = [];
    public List<Dream> Dreams { get; init; } = [];
    
}