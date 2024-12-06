using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Events.Models;
using Microsoft.AspNetCore.Identity;

namespace Jhooa.UI.Features.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? StripeCustomerId { get; init; }
    
    public List<Registration> Registrations { get; init; } = [];
    public List<Dream> Dreams { get; init; } = [];
    
}