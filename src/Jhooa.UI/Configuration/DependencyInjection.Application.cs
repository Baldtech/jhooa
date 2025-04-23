using Jhooa.UI.Managers;
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
        services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();
        services.AddScoped<RedirectManager>();
        services.AddFeatureFlags(config);
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        return services;
    }
    
    private static void AddFeatureFlags(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<FeatureFlags>(
            config.GetSection(FeatureFlags.SectionName));
    }
}