using Jhooa.UI.Data;
using Jhooa.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Jhooa.UI.Configuration;

public static partial class DependencyInjection
{
    private const string EmailDefaultFromEmailKey = "SendGrid:DefaultFromEmail";
    private const string SendGridApiKey = "SendGrid:ApiKey";
    
    /// <summary>
    ///     Adds infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetValue<string>("Database:ConnectionString") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddStripeIntegration(config);
        services.AddMessageServices(config);

        return services;
    }

    private static void AddStripeIntegration(this IServiceCollection services, IConfiguration config)
    {
        var stripeApiKey = config.GetValue<string>("Stripe:ApiKey") ??
                           throw new InvalidOperationException("Stripe API key not found.");
        Stripe.StripeConfiguration.ApiKey = stripeApiKey;

        services.Configure<StripeConfiguration>(
            config.GetSection(StripeConfiguration.SectionName));
        services.AddScoped<IStripeService, StripeService>();
    }
    
    private static IServiceCollection AddMessageServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMailService, MailService>();

        var defaultFromEmail = configuration.GetValue<string>(EmailDefaultFromEmailKey);
        var apiKey = configuration.GetValue<string>(SendGridApiKey);
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            services.AddFluentEmail(defaultFromEmail)
                .AddSendGridSender(apiKey);
        }
        else
        {
            throw new InvalidOperationException(
                "No email sender configuration found. Please configure either an Azure Communication Service connection string or endpoint.");
        }

        return services;
    }
}