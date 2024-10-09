using Jhooa.UI.Features.Identity.Models;

namespace Jhooa.UI.Features.Events.Models;

public class Registration
{
    public required Guid UserId { get; init; }
    public required Guid EventId { get; init; }
    
    public DateTimeOffset RegistrationDate { get; init; } = DateTimeOffset.Now;
    
    public ApplicationUser User { get; set; } = null!;
    public Event Event { get; set; } = null!;
}