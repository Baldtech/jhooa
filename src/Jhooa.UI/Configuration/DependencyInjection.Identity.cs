using Jhooa.UI.Data;
using Jhooa.UI.Features.Identity;
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
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        return services;
    }
}