using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Jobs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Jhooa.UI.Features.Identity;

internal sealed class IdentityEmailSender(IMediator mediator) : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        mediator.Publish(new EmailNotification(email, "AccountConfirmation", new
        {
            User_Name = user.FirstName,
            Confirmation_Link = confirmationLink
        }, "Confirm your email"));
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        mediator.Publish(new EmailNotification(email, "ResetPassword", new
        {
            User_Name = user.FirstName,
            Reset_Link = resetLink,
        }, "Reset your password"));
        return Task.CompletedTask;
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