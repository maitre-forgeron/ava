using Ava.Domain.Models.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ava.Infrastructure.Db.Configurations.User
{
    public class TherapistMapping : IEntityTypeConfiguration<Therapist>
    {
        public void Configure(EntityTypeBuilder<Therapist> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.Rating).HasColumnType("decimal(3,2)");

            builder.HasOne(x => x.UserProfile)
                .WithOne()
                .HasForeignKey<Therapist>(x => x.UserProfileId);
        }
    }
}
