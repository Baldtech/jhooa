namespace Jhooa.UI.Features.Events.Models;

public class Event
{
    public const int TitleMaxLength = 100;
    public const int ImageUrlMaxLength = 1000;
    public const int DescriptionMaxLength = 4000;
    
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Title { get; init; }
    public required string TitleEn { get; init; }
    public required string Description { get; init; }
    public required string DescriptionEn { get; init; }
    public string? ImageUrl { get; set; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    
    public List<Registration> Registrations { get; init; } = [];
    
    public bool IsPast => DateTimeOffset.UtcNow > StartDate;
}