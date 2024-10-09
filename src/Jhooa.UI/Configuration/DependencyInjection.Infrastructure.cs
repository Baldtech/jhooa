using Jhooa.UI.Data;
using Jhooa.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Jhooa.UI.Configuration;

public static partial class DependencyInjection
{
    /// <summary>
    ///     Adds infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var isDevelopment = string.Equals(config.GetValue<string>("ASPNETCORE_ENVIRONMENT"), "Development", StringComparison.OrdinalIgnoreCase);
        
        var connectionString = config.GetValue<string>("Database:ConnectionString") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        if (isDevelopment)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        
        services.AddDatabaseDeveloperPageExceptionFilter();
        
        
        services.AddStripeIntegration(config);

        return services;
    }
    
    private static void AddStripeIntegration(this IServiceCollection services, IConfiguration config)
    {
        var stripeApiKey = config.GetValue<string>("Stripe:ApiKey") ??
                           throw new InvalidOperationException("Stripe API key not found.");
        StripeConfiguration.ApiKey = stripeApiKey;
        
        services.AddScoped<IStripeService, StripeService>();
    }
}