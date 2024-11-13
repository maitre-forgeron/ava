using Ava.Domain.Models.Booking;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ava.Infrastructure.Db.Configurations.Bookings
{
    public class BookingMapping : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.Status).HasConversion<string>();
        }
    }
}
