using Ava.Domain.Models.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ava.Infrastructure.Db.Configurations.User
{
    public class ReviewMapping : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.RecipientId).IsRequired();

            builder.HasOne(x => x.Sender)
                .WithMany(x => x.SenderReviews)
                .HasForeignKey(x => x.SenderId);

            builder.HasOne(x => x.Recipient)
                .WithMany(x => x.RecipientReviews)
                .HasForeignKey(x => x.RecipientId);
        }
    }
}
