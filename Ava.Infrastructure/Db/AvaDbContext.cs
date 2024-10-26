using Ava.Domain.Models.Category;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ava.Infrastructure.Db;

public class AvaDbContext : IdentityDbContext
{
    public AvaDbContext(DbContextOptions<AvaDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Therapist> Therapists { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Category> Categories { get; set; }
}
