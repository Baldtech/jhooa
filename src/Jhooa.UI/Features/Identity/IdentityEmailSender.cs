using System.Text.Json;
using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace Jhooa.UI.Features.Identity;

internal sealed class IdentityEmailSender (IMailService mailService) : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        return mailService.SendWithTemplateAsync(email, "d-e34c8749182e49bfb4c27547774adb6d",
            new
            {
                User_Name = user.FirstName,
                Confirmation_Link = confirmationLink
            });
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        return mailService.SendWithTemplateAsync(email, "d-71b3a3ee1e604455ae589d58612a17a8",
            new
            {
                User_Name = user.FirstName,
                Reset_Link = resetLink
            });
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
#pragma warning disable MA0025
        throw new NotImplementedException();
#pragma warning restore MA0025
    }

    // public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
    //     _emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");
    //
    // public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
    //     _emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
}
