using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Jhooa.UI.Extensions;

public static class LogAndTelemetryExtensions
{
    public static void RegisterSerilogAndApplicationInsights(this WebApplicationBuilder builder)
    {
        var appInsightsConnectionString =
            builder.Configuration.GetValue<string>("ApplicationInsights:ConnectionString");
        
        if (!string.IsNullOrWhiteSpace(appInsightsConnectionString))
        {
            builder.Services.AddApplicationInsightsTelemetry();
        }
        
        builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            if (!string.IsNullOrWhiteSpace(appInsightsConnectionString))
            {
                var telemetryConfiguration = services.GetRequiredService<TelemetryConfiguration>();
                telemetryConfiguration.ConnectionString = appInsightsConnectionString;
                loggerConfiguration
                    .WriteTo.ApplicationInsights(
                        telemetryConfiguration: services.GetRequiredService<TelemetryConfiguration>(),
                        telemetryConverter: TelemetryConverter.Traces);
            }
            
            loggerConfiguration
                .MinimumLevel.Warning()
                .Enrich.FromLogContext()
                .Enrich.WithUserInfo()
                .Enrich.WithProperty("Environment", context.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT"));
            
            loggerConfiguration.ReadFrom.Configuration(context.Configuration);
        });
    }

    public static void RegisterSerilogAndApplicationInsights(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
    
    private static LoggerConfiguration WithUserInfo(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration.With<UserInfoEnricher>();
    }

    private sealed class UserInfoEnricher(IHttpContextAccessor httpContextAccessor) : ILogEventEnricher
    {
        public UserInfoEnricher() : this(new HttpContextAccessor())
        {
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "UserName", httpContextAccessor.HttpContext?.User.Identity?.Name ?? ""));
        }
    }
}