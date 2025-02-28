using Jhooa.UI.Data;
using Jhooa.UI.Features.Identity;
using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;

namespace Jhooa.UI.Configuration;

public static partial class DependencyInjection
{
    /// <summary>
    /// Adds auth services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

        services.AddAuthorization();
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(cookie =>
            {
                cookie.ApplicationCookie?.Configure(c => { c.Cookie.Name = Constants.Cookie.User; });
            });
        
        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>();
        
        services.AddTransient<IEmailSender<ApplicationUser>, IdentityEmailSender>();

        return services;
    }
}