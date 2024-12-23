using OpenShelter.Exceptions;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;

namespace OpenShelter.Configuration;

public static class OpenTelemetryServiceCollectionExtensions
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddLogging();

        String? otelEndpoint = config["OTEL_ENDPOINT"];
        if (otelEndpoint is null)
        {
            return services;
        }

        Uri otelUri;
        try
        {
            otelUri = new Uri(otelEndpoint);
        }
        catch (Exception)
        {
            throw new ConfigurationException($"invalid OTEL Collector endpoint: {otelEndpoint}");
        }

        services.AddOpenTelemetry()
            .UseOtlpExporter(OtlpExportProtocol.Grpc, otelUri)
            .WithLogging()
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
            );

        return services;
    }
}
