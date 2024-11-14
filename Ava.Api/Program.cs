using Ava.api.Extensions;
using Ava.Api.Extensions;
using Ava.Application;
using Ava.Infrastructure;
using Ava.Infrastructure.Db;
using Ava.Infrastructure.Services.Pictures;
using Ava.Logging;
using Serilog;
using System.Reflection;

namespace Ava.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLoggingServices();
        builder.Host.UseSerilog(SerilogConfigurator.Configure);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.AddAuthentication();

        builder.Configuration.AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetAssembly(typeof(PictureService))!, true);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<AvaDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        // Use the CORS policy
        app.UseCors("AllowLocalhost");

        app.MapControllers();

        app.Run();
    }
}
