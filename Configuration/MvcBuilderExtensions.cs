using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace OpenShelter.Configuration;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder WithInvalidModelStateLogging(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
                Func<ActionContext, IActionResult> responseFactory = options.InvalidModelStateResponseFactory;

                options.InvalidModelStateResponseFactory = context =>
                {
                    SerializableError serializeableError = new(context.ModelState);
                    String jsonErrors = JsonSerializer.Serialize(serializeableError);

                    ILogger<Program> logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError("ModelState errors: {JsonErrors}", jsonErrors); 

                    return responseFactory(context);
                };
        });

        return builder;
    }
}
