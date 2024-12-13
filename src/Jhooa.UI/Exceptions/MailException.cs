namespace Jhooa.UI.Exceptions;

public sealed class MailException(string to, string template, List<string> errors)
    : Exception($"Failed to send email to '{to}' with template '{template}' : {string.Join(", ", errors)}");