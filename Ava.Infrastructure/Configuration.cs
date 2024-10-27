using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Infrastructure.Repositories.UserRepositories;
using Ava.Infrastructure.Services.PictureService;

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

        //TODO inject repositories as scoped services
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IPictureService, PictureService>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITherapistRepository, TherapistRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }
}
