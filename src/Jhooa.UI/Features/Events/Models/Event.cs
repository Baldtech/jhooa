namespace Jhooa.UI.Features.Events.Models;

public class Event
{
    public const int TitleMaxLength = 100;
    public const int ImageUrlMaxLength = 100;
    public const int DescriptionMaxLength = 4000;
    
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Title { get; init; }
    public required string Description { get; init; }
    public string? ImageUrl { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    
    public List<Registration> Registrations { get; init; } = [];
}