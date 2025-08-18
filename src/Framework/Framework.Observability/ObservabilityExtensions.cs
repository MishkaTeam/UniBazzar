using Framework.Observability.Enrichers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Sinks.Grafana.Loki;

namespace Framework.Observability;

public static class ObservabilityExtensions
{
    public static WebApplicationBuilder AddObservability
        (this WebApplicationBuilder builder, string applicationName, string version, Action<EnrichmentOptions>? configureEnrichment = null)
    {
        var options = builder.Configuration
            .GetSection(ObservabilityOptions.SectionName)
            .Get<ObservabilityOptions>() ?? new ObservabilityOptions();

        var resourceBuilder = ResourceBuilder.CreateDefault()
            .AddService(serviceName: applicationName, serviceVersion: version);

        builder.Services.AddHttpContextAccessor();

        var enrichmentOptions = new EnrichmentOptions();
        // Execute the configuration action provided by the consumer (SampleWebApp)
        configureEnrichment?.Invoke(enrichmentOptions);

        builder.Host.UseSerilog((context, services, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithSpan()
                .WriteTo.Console()
                .WriteTo.GrafanaLoki(
                    uri: options.LokiUrl,
                    labels: new[] { new LokiLabel { Key = "app", Value = applicationName } }
                );

            if (enrichmentOptions.CustomContextProvider != null)
            {
                loggerConfig.Enrich.With(new CustomContextEnricher(
                    services.GetRequiredService<IHttpContextAccessor>(),
                    enrichmentOptions.CustomContextProvider));
            }
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation() 
                    .AddHttpClientInstrumentation()
                    .AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation() 
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(efOptions =>
                    {
                        efOptions.SetDbStatementForText = true; 
                    })
                    .AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri(options.OtlpEndpoint);
                    });
            });

        return builder;
    }

    public static IApplicationBuilder UseObservability(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        app.UseOpenTelemetryPrometheusScrapingEndpoint();

        return app;
    }
}