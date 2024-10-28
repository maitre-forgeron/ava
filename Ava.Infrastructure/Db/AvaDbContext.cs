using Ava.Domain.Models.Category;
using Ava.Domain.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ava.Infrastructure.Db;

public class AvaDbContext : IdentityDbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Therapist> Therapists { get; set; }

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
