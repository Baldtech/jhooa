using Azure.Identity;
using Azure.Storage.Blobs;
using Jhooa.UI.Data;
using Jhooa.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace Jhooa.UI.Configuration;

public static partial class DependencyInjection
{
    private const string EmailDefaultFromEmailKey = "Email:DefaultFromEmail";
    private const string EmailModeKey = "Email:Mode";
    private const string SendGridApiKey = "Email:SendGridApiKey";
    private const string AzureCommunicationServiceEndpointKey = "Email:AzureCommunicationServiceEndpoint";

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
            options.UseSqlServer(connectionString), ServiceLifetime.Transient);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddStripeIntegration(config)
            .AddMessageServices(config)
            .AddAzureServices(config);

        return services;
    }

    private static IServiceCollection AddStripeIntegration(this IServiceCollection services, IConfiguration config)
    {
        var stripeApiKey = config.GetValue<string>("Stripe:ApiKey") ??
                           throw new InvalidOperationException("Stripe API key not found.");
        Stripe.StripeConfiguration.ApiKey = stripeApiKey;

        services.Configure<StripeConfiguration>(
            config.GetSection(StripeConfiguration.SectionName));
        services.AddScoped<IStripeService, StripeService>();

        return services;
    }

    private static IServiceCollection AddMessageServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mode = configuration.GetValue<string>(EmailModeKey);

        var defaultFromEmail = configuration.GetValue<string>(EmailDefaultFromEmailKey);

        var apiKey = configuration.GetValue<string>(SendGridApiKey);

        var endpoint = configuration.GetValue<string>(AzureCommunicationServiceEndpointKey);

        if (string.Equals(mode, "SendGrid", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(apiKey))
        {
            services.AddSingleton<IMailService, SendGridMailService>();

            services.AddFluentEmail(defaultFromEmail)
                .AddSendGridSender(apiKey);
        }
        else if (string.Equals(mode, "Azure", StringComparison.OrdinalIgnoreCase) &&
                 !string.IsNullOrWhiteSpace(endpoint))
        {
            services.AddSingleton<IMailService, AzureMailService>();

            services.AddFluentEmail(defaultFromEmail)
                .AddAzureEmailSender(new Uri(endpoint), new DefaultAzureCredential());
        }
        else
        {
            throw new InvalidOperationException(
                "No email sender configuration found. Please configure either an Azure Communication Service endpoint or SendGrid.");
        }

        return services;
    }

    private static IServiceCollection AddAzureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(builder =>
        {
            builder.UseCredential(new DefaultAzureCredential());
            builder.AddBlobServiceClient(new Uri(configuration.GetValue<string>("Blob:Storage")!));
        });
        
        services.AddSingleton<IBlobService, BlobService>(provider => new BlobService(
            provider.GetRequiredService<IAzureClientFactory<BlobServiceClient>>(),
            provider.GetRequiredService<ILogger<BlobContainerService>>(),
            "Default"));

        return services;
    }
}