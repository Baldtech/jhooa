using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.SendGrid;

namespace Jhooa.UI.Services;

public class MailService(ILogger<MailService> logger, IFluentEmail fluentEmail) : IMailService
{
    public async Task<SendResponse> SendWithTemplateAsync(string to, string templateId, object templateData)
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

    private sealed class MailException(string to, string template, List<string> errors)
        : Exception($"Failed to send email to '{to}' with template '{template}' : {string.Join(", ", errors)}");
}