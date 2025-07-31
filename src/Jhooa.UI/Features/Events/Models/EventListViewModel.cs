namespace Jhooa.UI.Features.Events.Models;

public class EventListViewModel
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? ImageUrl { get; set; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public bool IsVisible { get; set; }
    public bool IsPast => DateTimeOffset.Now.Date > EndDate;

}