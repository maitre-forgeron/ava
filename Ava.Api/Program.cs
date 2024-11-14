
using Ava.api.Extensions;
using Ava.Api.Services;
using Ava.Application;
using Ava.Application.Contracts;
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

        // Add services to the container.
        builder.Services.AddLoggingServices();
        builder.Host.UseSerilog(SerilogConfigurator.Configure);

        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddScoped<IUserClaimService, UserClaimService>();

        builder.AddAuthentication();
       
        builder.Configuration.AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetAssembly(typeof(PictureService))!, true);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
