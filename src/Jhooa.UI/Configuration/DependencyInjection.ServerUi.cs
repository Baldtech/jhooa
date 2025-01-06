using BytexDigital.Blazor.Components.CookieConsent;
using Jhooa.UI.Extensions;
using Jhooa.UI.Features.Identity;
using Jhooa.UI.Pages;
using Microsoft.AspNetCore.Localization;

namespace Jhooa.UI.Configuration;

public static partial class DependencyInjection
{
    /// <summary>
    /// Adds server UI services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddServerUi(this IServiceCollection services, IConfiguration config)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddHubOptions(options => options.MaximumReceiveMessageSize = 64 * 1024);

        services.AddProblemDetails();
        services.AddHealthChecks();

        services.AddControllers();

        services.AddLocalization();
        
        services.ConfigureCookie();

        services.AddAntiforgery(options =>
        {
            options.FormFieldName = "AntiforgeryHiddenField";
            options.Cookie.Name = Constants.Cookie.Xsrf;
            options.HeaderName = Constants.Cookie.XsrfHeader;
        });

        return services;
    }

#pragma warning disable MA0051
    private static void ConfigureCookie(this IServiceCollection services)
#pragma warning restore MA0051
    {
        services.AddCookieConsent(o =>
        {
            o.Revision = 1;
            o.PolicyUrl = "/cookie-policy";
            o.CookieOptions = new CookieConsentCookieOptions()
            {
                CookieName = "CookieConsent",
            };
            
            // Call optional
            o.UseDefaultConsentPrompt(prompt =>
            {
                prompt.Position = ConsentModalPosition.BottomRight;
                prompt.Layout = ConsentModalLayout.Bar;
                prompt.SecondaryActionOpensSettings = false;
                prompt.AcceptAllButtonDisplaysFirst = false;
            });
        });
    }

    /// <summary>
    /// Configures the server pipeline.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <param name="config">The configuration.</param>
    /// <returns>The configured web application.</returns>
    public static void ConfigureServer(this WebApplication app, IConfiguration config)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }
        
        app.RegisterSerilogAndApplicationInsights();

        app.UseHttpsRedirection();

        var supportedCultures = new[] { "en-GB", "fr-FR" };
        var localizationOptions = new RequestLocalizationOptions()
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
        
        localizationOptions.AddInitialRequestCultureProvider(new CookieRequestCultureProvider
        {
            CookieName = Constants.Cookie.Culture,
        });

        app.UseRequestLocalization(localizationOptions);

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapControllers();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapAdditionalIdentityEndpoints();
    }
}