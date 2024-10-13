using Ava.Domain.Models.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ava.Infrastructure.Db.Configurations.User
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate).IsRequired();

            builder.HasOne(x => x.UserProfile)
                .WithOne()
                .HasForeignKey<Customer>(x => x.UserProfileId);
        }
    }
}
