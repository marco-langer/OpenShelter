namespace OpenShelter.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        return app;
    }
}
