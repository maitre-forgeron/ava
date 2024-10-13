
using Ava.Application;
using Ava.Infrastructure;
using Ava.Infrastructure.Db;
using Ava.Infrastructure.Models;
using Ava.Infrastructure.Services.PictureService;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Ava.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AvaDbContext>()
            .AddDefaultTokenProviders();

        builder.Configuration.AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetAssembly(typeof(PictureService))!, true);


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
