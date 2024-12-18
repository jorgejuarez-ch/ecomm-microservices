using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database")!;

        //// Add services to the container.
        // services.AddDbContext<ApplicationDbContext>(options =>
        //      options.UseSQlServer(connectionString));

        // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // app.MapCarter();

        return app;
    }
}
