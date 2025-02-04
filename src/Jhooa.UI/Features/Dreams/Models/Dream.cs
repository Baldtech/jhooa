using Jhooa.UI.Features.Identity.Models;

namespace Jhooa.UI.Features.Dreams.Models;

public class Dream
{
    public const int TitleMaxLength = 300;
    
    public Guid Id { get; init; } = Guid.NewGuid();
    public required DreamTypes Type { get; init; } = DreamTypes.Small;
    public required string Title { get; init; }
    public required Guid UserId { get; init; }
    public ApplicationUser User { get; set; } = null!;

}