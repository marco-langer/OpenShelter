using OpenShelter.Configuration;
using OpenShelter.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.ConfigureLogging();

builder.Services.AddOpenTelemetry(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddControllers().WithInvalidModelStateLogging();
builder.Services.AddOpenApi();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IShelterRepository, ShelterRepository>();

WebApplication app = builder.Build();

app.UseOpenApi();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
