namespace OpenShelter.Configuration;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder loggingBuilder)
    {
        // a new logger provider will be set via the Open Telemetry Service
        loggingBuilder.ClearProviders();

        return loggingBuilder;
    }
}
