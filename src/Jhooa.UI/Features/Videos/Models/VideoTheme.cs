namespace Jhooa.UI.Features.Videos.Models;

public class VideoTheme
{
    public required Guid Id { get; init; }
    public required string NameEn { get; init; }
    public required string NameFr { get; init; }
    public List<Video> Videos { get; init; } = [];

}