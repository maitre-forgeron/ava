using Ava.Application.Bookings.Commands;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Domain.Models.Booking;
using Ava.Infrastructure.Db;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace Ava.Tests.Unit.Commands
{
    [Collection("Booking_Collection")]
    public class BookingCommandShould
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<AvaDbContext> _mockDbContext;
        private readonly Mock<DbSet<Booking>> _mockDbSet;
        public BookingCommandShould(ServiceProviderFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
            _mockDbContext = new Mock<AvaDbContext>();
            _mockDbSet = new Mock<DbSet<Booking>>();

            _mockDbContext.Setup(x => x.Bookings).Returns(_mockDbSet.Object);
        }

        [Fact]
        public async Task CreateSingleBooking()
        {
            // Arrange
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes
            var resultToReturn = Result.Success();

            _mockDbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
              .ReturnsAsync(1);
            _mockDbContext.Setup(m => m.Add(It.IsAny<Booking>())).Verifiable();

            var command = new AddBookingCommand(new CreateBookingDto(consumerId, therapistId, preferredTime, duration));
            var commandHandler = new AddBookingCommandHandler(_mockDbContext.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mockDbContext.Verify(m => m.Add(It.IsAny<Booking>()), Times.Once);
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
