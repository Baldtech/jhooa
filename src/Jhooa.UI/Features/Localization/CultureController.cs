using System.Security.Claims;
using Jhooa.UI.Configuration;
using Jhooa.UI.Data;
using Jhooa.UI.Features.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Jhooa.UI.Features.Localization;

[Route("[controller]/[action]")]
public class CultureController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
{
    public async Task<IActionResult> Set(string? culture, string redirectUri)
    {
        if (culture == null) 
            return LocalRedirect(redirectUri);
        
        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await userManager.GetUserAsync(User);
            if (user is not null)
            {
                await ResetUserClaim(culture, user);
            }
        }

        HttpContext.Response.Cookies.Append(
            Constants.Cookie.Culture,
            CookieRequestCultureProvider.MakeCookieValue(
                new RequestCulture(culture, culture)));

        return LocalRedirect(redirectUri);
    }

    private async Task ResetUserClaim(string culture, ApplicationUser user)
    {
        var currentClaim =
            User.Claims.FirstOrDefault(c => string.Equals(c.Type, Constants.Cookie.Culture, StringComparison.OrdinalIgnoreCase));

        if (currentClaim != null)
        {
            await userManager.RemoveClaimAsync(user, currentClaim);
        }
        await userManager.AddClaimAsync(user, new Claim(Constants.Cookie.Culture, culture));
        
        await signInManager.SignInAsync(user, isPersistent: false);
    }
}