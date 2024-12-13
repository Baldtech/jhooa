using FluentEmail.Core.Models;

namespace Jhooa.UI.Services;

public interface IMailService
{
    Task<SendResponse> SendWithTemplateAsync(string to, string templateId, object templateData, string? subject = default);


}