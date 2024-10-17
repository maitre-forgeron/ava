using Ava.Domain.Models.Category;

namespace Ava.Infrastructure.Db;

public class AvaDbContext : DbContext
{
    //TODO DbSets here
    public DbSet<Category> Categories { get; set; }

    public AvaDbContext(DbContextOptions<AvaDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
