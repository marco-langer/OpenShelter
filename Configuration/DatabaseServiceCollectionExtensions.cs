using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace OpenShelter.Configuration;

public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddDbContext<ShelterDbContext>(options => options
            .UseNpgsql(config.GetConnectionString("DefaultConnection"))
            .UseExceptionProcessor());

        return services;
    }
}
