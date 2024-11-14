using Ava.Application.Bookings.Queries;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Domain.Models.Booking;
using Ava.Infrastructure.Db;

using Microsoft.EntityFrameworkCore;

using Moq;
using Moq.EntityFrameworkCore;

namespace Ava.Tests.Unit.Queries;

[Collection("Booking_Collection")]
public class BookingQueryShould
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Mock<AvaDbContext> _mockDbContext;
    private readonly Mock<DbSet<Booking>> _mockDbSet;
    public BookingQueryShould(ServiceProviderFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
        _mockDbContext = new Mock<AvaDbContext>();
        _mockDbSet = new Mock<DbSet<Booking>>();

        _mockDbContext.Setup(x => x.Bookings).Returns(_mockDbSet.Object);
    }

    [Fact]
    public async Task GetSingleBooking()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var resultToReturn = Result.Success();

        var booking = new Booking(bookingId, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, 30);

        var expectedBookingDto = new BookingDto(
            booking.Id,
            booking.ConsumerId,
            booking.TherapistId,
            booking.PreferredTime,
            booking.Duration,
            booking.Status,
            booking.StatusChangeTime);

        _mockDbContext.Setup(c => c.Bookings).ReturnsDbSet(new List<Booking> { booking });

        var query = new GetBookingQuery(bookingId);
        var queryHandler = new GetBookingQueryHandler(_mockDbContext.Object);

        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedBookingDto.Id, result.Id);
    }
}
