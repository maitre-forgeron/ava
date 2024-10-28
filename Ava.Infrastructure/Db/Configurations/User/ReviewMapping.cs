using Ava.Domain.Models.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ava.Infrastructure.Db.Configurations.User;

public class ReviewMapping : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasOne(x => x.Author)
            .WithMany(x => x.SenderReviews)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Recipient)
            .WithMany(x => x.RecipientReviews)
            .HasForeignKey(x => x.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
