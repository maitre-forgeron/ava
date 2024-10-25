﻿using Ava.Domain.Models.User;
using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Ava.Domain.Models.Category;

namespace Ava.Infrastructure.Db;

public class AvaDbContext : IdentityDbContext
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

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Therapist> Therapists { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
}
