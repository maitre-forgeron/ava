namespace Ava.Infrastructure.Db;

public class AvaDbContextFactory : IDesignTimeDbContextFactory<AvaDbContext>
{
    public AvaDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("AvaConnectionString");

        var optionsBuilder = new DbContextOptionsBuilder<AvaDbContext>()
            .UseSqlServer(connectionString);

        return new AvaDbContext(optionsBuilder.Options);
    }

}
