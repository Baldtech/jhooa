using Jhooa.UI.Components;

namespace Jhooa.UI;

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

        services.AddAntiforgery(options =>
        {
            options.FormFieldName = "AntiforgeryHiddenField";
            options.Cookie.Name = Constants.Cookie.Xsrf;
            options.HeaderName = Constants.Cookie.XsrfHeader;
        });

        return services;
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

        app.UseHttpsRedirection();

        var supportedCultures = new[] { "en-GB", "fr-FR" };
        var localizationOptions = new RequestLocalizationOptions()
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapControllers();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapAdditionalIdentityEndpoints();
    }
}