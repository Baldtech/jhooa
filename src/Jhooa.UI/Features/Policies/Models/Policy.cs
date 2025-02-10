namespace Jhooa.UI.Features.Policies.Models;

public class Policy
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required PolicyType Type { get; set; }
    public required string TitleFr { get; set; }
    public required string TitleEn { get; set; }
    public required string ContentFr { get; set; }
    public required string ContentEn { get; set; }
    public required DateTimeOffset LastUpdate { get; set; }
}