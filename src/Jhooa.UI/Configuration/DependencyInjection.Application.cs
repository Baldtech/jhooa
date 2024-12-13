using Jhooa.UI.Services;

namespace Jhooa.UI.Configuration;

public static partial class DependencyInjection
{
    /// <summary>
    /// Adds application services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton(services);
        
        services.AddScoped<IDreamService, DreamService>();


        return services;
    }
}