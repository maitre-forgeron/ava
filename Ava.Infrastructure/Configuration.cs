using Ava.Infrastructure.Services.Identity;
using Ava.Infrastructure.Services.Pictures;

namespace Ava.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var x = configuration.GetConnectionString("AvaConnectionString");
        services.AddDbContext<AvaDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AvaConnectionString"), builder =>
            {
                builder.EnableRetryOnFailure();
            });
        });

        services.AddScoped<AvaDbContextInitialiser>();

        services.AddSingleton<IPictureService, PictureService>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
