using Jhooa.UI.Services;
using MediatR;

namespace Jhooa.UI.Jobs;

public record EmailNotification(string To, string TemplateId, object TemplateData, string Subject) : INotification;
public record EmailNotificationWithCulture(string To, CultureInfo CultureInfo, object TemplateData, string Subject) : INotification;

public class EmailSender(IMailService mailService) : INotificationHandler<EmailNotification>, INotificationHandler<EmailNotificationWithCulture>
{
    public async Task Handle(EmailNotification notification, CancellationToken cancellationToken)
    {
        await mailService.SendWithTemplateAsync(notification.To, notification.TemplateId,
            notification.TemplateData, notification.Subject);
    }

    public async Task Handle(EmailNotificationWithCulture notification, CancellationToken cancellationToken)
    {
        await mailService.SendWithTemplateAsync(notification.To, notification.CultureInfo,
            notification.TemplateData, notification.Subject);
    }
}