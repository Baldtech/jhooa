using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.SendGrid;
using Jhooa.UI.Exceptions;

namespace Jhooa.UI.Services;

public class SendGridMailService(ILogger<SendGridMailService> logger, IFluentEmail fluentEmail) : IMailService
{
    public async Task<SendResponse> SendWithTemplateAsync(string to, string templateId, object templateData, string? subject = default)
    {
        try
        {
            var result = await fluentEmail
                .To(to)
                .SendWithTemplateAsync(templateId, templateData);

            if (!result.Successful)
            {
                throw new MailException(to, templateId, result.ErrorMessages.ToList());
            }

            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to send email. To: {EmailTo}", to);
            throw;
        }
    }

    
}