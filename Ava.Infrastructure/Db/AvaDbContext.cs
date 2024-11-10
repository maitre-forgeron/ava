using Ava.Domain.Models.Booking;
using Ava.Domain.Models.Category;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ava.Infrastructure.Db;

public class AvaDbContext :
    IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Therapist> Therapists { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<UserProfile> UserProfiles { get; set; }

    public DbSet<User> Users { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public DbSet<TherapistCategory> TherapistCategories { get; set; }

    public AvaDbContext()
    {

    }

    public AvaDbContext(DbContextOptions<AvaDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
