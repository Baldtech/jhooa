namespace Jhooa.UI.Features.Subscriptions.Models.DTO;

public class PaymentIntentResponse
{
    public required string SessionUrl { get; init; }
    public required string SessionId { get; init; }
}