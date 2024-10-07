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

        return services;
    }
}
