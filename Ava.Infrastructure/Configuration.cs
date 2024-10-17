using Ava.Infrastructure.Services.PictureService;

namespace Ava.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AvaDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AvaConnectionString"), builder =>
            {
                builder.EnableRetryOnFailure();
            });
        });

        services.AddScoped<AvaDbContextInitialiser>();

        //TODO inject repositories as scoped services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IPictureService, PictureService>();

        SeedDatabase(services.BuildServiceProvider());

        return services;
    }

    private static void SeedDatabase(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<AvaDbContextInitialiser>();
            initializer.InitialiseAsync().GetAwaiter().GetResult();
            initializer.SeedAsync().GetAwaiter().GetResult();
        }
    }
}
