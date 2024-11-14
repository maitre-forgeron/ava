using Ava.Api.Controllers;
using Ava.Application.Bookings.Commands;
using Ava.Application.Bookings.Queries;
using Ava.Application.Constants;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Domain.Models.Booking;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

namespace Ava.Tests.Unit.Controllers
{
    [Collection("Booking_Collection")]
    public class BookingControllerShould
    {
        private readonly IServiceProvider _serviceProvider;
        public BookingControllerShould(ServiceProviderFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async Task GetSingleBooking()
        {
            // Arrange
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes
            var status = BookingStatus.InProgress;

            var bookingDtoToReturn = new BookingDto(Guid.NewGuid(), consumerId, therapistId, preferredTime, duration, status, null);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetBookingQuery>(), default)).ReturnsAsync(bookingDtoToReturn);

            var bookingController = new BookingController(mediatorMock.Object);

            // Act
            var result = await bookingController.GetBooking(bookingDtoToReturn.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookingDto = Assert.IsAssignableFrom<BookingDto>(okResult.Value);

            Assert.NotNull(bookingDto);
            Assert.Equal(bookingDtoToReturn.Id, bookingDto.Id);
            Assert.IsAssignableFrom<BookingDto>(bookingDto);
        }

        [Fact]
        public async Task CreateSingleBooking()
        {
            // Arrange
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes

            var createBookingDto = new CreateBookingDto(consumerId, therapistId, preferredTime, duration);
            var resultToReturn = Result.Success();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddBookingCommand>(), default)).ReturnsAsync(resultToReturn);

            var bookingController = new BookingController(mediatorMock.Object);

            // Act
            var result = await bookingController.AddBooking(createBookingDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result); 
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteSingleBooking()
        {
            // Arrange
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes

            var bookingEntity = new Booking(Guid.NewGuid(), consumerId, therapistId, preferredTime, duration);
            var resultToReturn = Result.Success();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBookingCommand>(), default)).ReturnsAsync(resultToReturn);

            var bookingController = new BookingController(mediatorMock.Object);

            // Act
            var result = await bookingController.DeleteBooking(bookingEntity.Id);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task FailToDeleteSingleBooking()
        {
            // Arrange
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes

            var bookingEntity = new Booking(Guid.NewGuid(), consumerId, therapistId, preferredTime, duration);
            var resultToReturn = Result.Failure(BookingErrors.NotFound);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBookingCommand>(), default)).ReturnsAsync(resultToReturn);

            var bookingController = new BookingController(mediatorMock.Object);

            // Act
            var result = await bookingController.DeleteBooking(bookingEntity.Id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<Error>(badRequestResult.Value);
            Assert.Equal("Booking was not found!", error.Description);
        }

        [Fact]
        public async Task ApproveSingleBooking()
        {
            // Arrange
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes

            var bookingEntity = new Booking(Guid.NewGuid(), consumerId, therapistId, preferredTime, duration);
            var resultToReturn = Result.Success();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<ApproveBookingCommand>(), default)).ReturnsAsync(resultToReturn);

            var bookingController = new BookingController(mediatorMock.Object);

            // Act
            var result = await bookingController.ApproveBooking(bookingEntity.Id);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task RejectSingleBooking()
        {
            // Arrange
            var consumerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var preferredTime = DateTime.UtcNow;
            var duration = 30; // minutes

            var bookingEntity = new Booking(Guid.NewGuid(), consumerId, therapistId, preferredTime, duration);
            var resultToReturn = Result.Success();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<RejectBookingCommand>(), default)).ReturnsAsync(resultToReturn);

            var bookingController = new BookingController(mediatorMock.Object);

            // Act
            var result = await bookingController.RejectBooking(bookingEntity.Id);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
