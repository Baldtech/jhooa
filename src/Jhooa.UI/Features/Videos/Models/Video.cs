namespace Jhooa.UI.Features.Videos.Models;

public class Video
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset Date { get; init; }
    public required string SubscribedVimeoId { get; init; }
    public required string SubscribedVimeoHash { get; init; }
    public required string NotSubscribedVimeoId { get; init; }
    public required string NotSubscribedVimeoHash { get; init; }
    public List<VideoTheme> Themes { get; private set; } = [];
    
    public void ResetThemes(List<VideoTheme> themes)
    {
        Themes = themes;
    }
}