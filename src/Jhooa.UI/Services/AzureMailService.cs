using System.Text.RegularExpressions;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Jhooa.UI.Exceptions;

namespace Jhooa.UI.Services;

public class AzureMailService(ILogger<AzureMailService> logger, IFluentEmail fluentEmail) : IMailService
{
    public async Task<SendResponse> SendWithTemplateAsync(string to, string templateId, object templateData, string? subject = default)
    {
        try
        {
            var filePath = $"./Resources/EmailTemplates/{templateId}.html";
            var htmlContent = await File.ReadAllTextAsync(filePath);
            
#pragma warning disable MA0009
            htmlContent = Regex.Replace(htmlContent, @"\{\{(\w+)\}\}", match =>
            {
                var key = match.Groups[1].Value; // Extract the tagname
                var property = templateData.GetType().GetProperty(key); // Get the property by name
                return property != null ? property.GetValue(templateData)?.ToString() ?? match.Value : match.Value;
            });
#pragma warning restore MA0009
            
            var result = await fluentEmail
                .To(to)
                .Subject(subject)
                .Body(htmlContent, isHtml: true)
                .SendAsync();

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

    public async Task<SendResponse> SendWithTemplateAsync(string to, CultureInfo cultureInfo, object templateData, string? subject = default)
    {
        try
        {
            var templateId = cultureInfo.Name.StartsWith("fr", StringComparison.OrdinalIgnoreCase) ? "template-fr.html" : "template-en.html";
            
            var filePath = $"./Resources/EmailTemplates/{templateId}";
            var htmlContent = await File.ReadAllTextAsync(filePath);
            
#pragma warning disable MA0009
            htmlContent = Regex.Replace(htmlContent, @"\{\{(\w+)\}\}", match =>
            {
                var key = match.Groups[1].Value; // Extract the tagname
                var property = templateData.GetType().GetProperty(key); // Get the property by name
                return property != null ? property.GetValue(templateData)?.ToString() ?? match.Value : match.Value;
            });
#pragma warning restore MA0009
            
            var result = await fluentEmail
                .To(to)
                .Subject(subject)
                .Body(htmlContent, isHtml: true)
                .SendAsync();

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