using Ava.Domain.Models.Booking;

namespace Ava.Tests.Unit.Models
{
    [Collection("Booking_Collection")]
    public class BookingShould
    {
        private readonly IServiceProvider _serviceProvider;
        public BookingShould(ServiceProviderFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void ConstructedWithValidData()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes
            var statusToCheck = BookingStatus.InProgress;

            // Act
            var booking = new Booking(bookingId, consumerId, therapistId, preferredTime, duration);

            // Assert 
            Assert.Equal(bookingId, booking.Id);
            Assert.Equal(consumerId, booking.ConsumerId);
            Assert.Equal(therapistId, booking.TherapistId);
            Assert.Equal(preferredTime, booking.PreferredTime);
            Assert.Equal(duration, booking.Duration);
            Assert.Equal(statusToCheck, booking.Status);
        }

        [Fact]
        public void UpdateStatusToAccepted()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes
            var statusToCheck = BookingStatus.Accepted;

            // Act
            var booking = new Booking(bookingId, consumerId, therapistId, preferredTime, duration);
            booking.Approve();

            // Assert
            Assert.Equal(statusToCheck, booking.Status);
            Assert.True(booking.StatusChangeTime != null);
        }

        [Fact]
        public void UpdateStatusToRejected()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes
            var statusToCheck = BookingStatus.Rejected;

            // Act
            var booking = new Booking(bookingId, consumerId, therapistId, preferredTime, duration);
            booking.Reject();

            // Assert
            Assert.Equal(statusToCheck, booking.Status);
            Assert.True(booking.StatusChangeTime != null);
        }
    }
}
