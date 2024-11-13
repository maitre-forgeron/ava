using Ava.Api.Controllers;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Application.Reviews.Commands;
using Ava.Application.Reviews.Queries;
using Ava.Application.Therapists.Commands;
using Ava.Application.Therapists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Ava.Tests.Unit.Controllers
{
    [Collection("Therapist_Collection")]
    public class TherapistControllerShould
    {
        private readonly IServiceProvider _serviceProvider;

        public TherapistControllerShould(ServiceProviderFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async Task ReturnNotFoundWhenNoTherapistsExist()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCustomersQuery>(), default))
                        .ReturnsAsync(Enumerable.Empty<TherapistDto>());
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.GetAllTherapists();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ReturnTherapistProfile()
        {
            // Arrange
            var therapistId = Guid.NewGuid();
            var therapistProfile = new TherapistDto(therapistId, 4.5, "Good Therapist", Guid.NewGuid(), new List<ReviewDto>());
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTherapistProfileQuery>(), default))
                        .ReturnsAsync(therapistProfile);
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.GetTherapistProfile(therapistId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProfile = Assert.IsType<TherapistDto>(okResult.Value);
            Assert.Equal(therapistId, returnedProfile.Id);
        }

        [Fact]
        public async Task ReturnBadRequestWhenTherapistDataIsNullOnAdd()
        {
            // Arrange
            CreateTherapistDto therapistDto = null;
            var mediatorMock = new Mock<IMediator>();
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.AddTherapist(therapistDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Therapist data is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task ReturnCreatedAtActionWhenTherapistIsAddedSuccessfully()
        {
            // Arrange
            var therapistDto = new CreateTherapistDto(Guid.NewGuid(),
                "Jane",
                "Doe",
                "123456789",
                4.5,
                "Experienced therapist"
            );

            var therapistId = Guid.NewGuid();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddTherapistCommand>(), default))
                        .ReturnsAsync(Result.Success());

            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.AddTherapist(therapistDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTherapistProfile", createdResult.ActionName);

            var firstRouteValue = createdResult.RouteValues.Values.First();
            var resultValue = Assert.IsType<Result>(firstRouteValue);

            Assert.True(resultValue.IsSuccess);
        }

        [Fact]
        public async Task ReturnBadRequestWhenUpdatingInvalidTherapist()
        {
            // Arrange
            var invalidTherapist = new UpdateTherapistDto(Guid.NewGuid(), "John", "Doe", 4.5, "Experienced therapist");
            var failureResult = Result.Failure(new Error("400", "Invalid therapist data"));
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTherapistCommand>(), default))
                        .ReturnsAsync(failureResult);
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateTherapist(invalidTherapist);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<Error>(badRequestResult.Value);
            Assert.Equal("Invalid therapist data", error.Description);
        }


        [Fact]
        public async Task ReturnNoContentWhenTherapistIsUpdatedSuccessfully()
        {
            // Arrange
            var updateTherapistDto = new UpdateTherapistDto(
                Guid.NewGuid(), "Updated FirstName", "Updated LastName", 4.5, "Updated summary of the therapist");

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTherapistCommand>(), default))
                        .ReturnsAsync(Result.Success());

            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateTherapist(updateTherapistDto);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task ReturnNoContentWhenTherapistIsDeletedSuccessfully()
        {
            // Arrange
            var therapistId = Guid.NewGuid();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTherapistCommand>(), default))
                        .ReturnsAsync(Result.Success());
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.DeleteTherapist(therapistId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ReturnOkWhenGettingRatingSummary()
        {
            // Arrange
            var therapistId = Guid.NewGuid();
            var ratingSummary = new RatingSummary(4.5, 2);
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetRatingSummaryQuery>(), default))
                        .ReturnsAsync(ratingSummary);
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.GetRatingSummary(therapistId, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSummary = Assert.IsType<RatingSummary>(okResult.Value);
            Assert.Equal(4.5, returnedSummary.AverageRating);
            Assert.Equal(2, returnedSummary.TotalReviews);
        }


        [Fact]
        public async Task ReturnOkWhenGettingAllReviewsForTherapist()
        {
            // Arrange
            var therapistId = Guid.NewGuid();
            var reviews = new List<ReviewDto>
            {
                new ReviewDto(Guid.NewGuid(), Guid.NewGuid(), therapistId, 4, "Great service!")
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetMoreReviewsForTherapistQuery>(), default))
                        .ReturnsAsync(reviews);

            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.GetAllReviews(therapistId, 0, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReviews = Assert.IsType<List<ReviewDto>>(okResult.Value);
            Assert.Single(returnedReviews);
            Assert.Equal(4, returnedReviews[0].Rating);
            Assert.Equal("Great service!", returnedReviews[0].Summary);
        }

        [Fact]
        public async Task ReturnCreatedAtActionWhenReviewIsAddedSuccessfully()
        {
            // Arrange
            var validReviewDto = new CreateReviewDto(Guid.NewGuid(), Guid.NewGuid(), 4, "Good service");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddReviewCommand>(), default))
                        .ReturnsAsync(Result.Success());
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.AddReviewToTherapist(validReviewDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTherapistProfile", createdResult.ActionName);
        }

        [Fact]
        public async Task ReturnBadRequestWhenAddingReviewWithInvalidData()
        {
            // Arrange
            var invalidReviewDto = new CreateReviewDto(Guid.Empty, Guid.NewGuid(), 5, "");
            var mediatorMock = new Mock<IMediator>();
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.AddReviewToTherapist(invalidReviewDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ReturnBadRequestWhenUpdatingReviewWithInvalidData()
        {
            // Arrange
            var invalidUpdateReviewDto = new UpdateReviewDto(Guid.NewGuid(), Guid.NewGuid(), 0, "");
            var mediatorMock = new Mock<IMediator>();
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateReview(invalidUpdateReviewDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var validationErrors = Assert.IsType<List<ValidationResult>>(badRequestResult.Value);
            Assert.NotEmpty(validationErrors);
        }

        [Fact]
        public async Task ReturnOkWhenReviewIsUpdatedSuccessfully()
        {
            // Arrange
            var validUpdateReviewDto = new UpdateReviewDto(Guid.NewGuid(), Guid.NewGuid(), 5, "Updated feedback");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateReviewCommand>(), default))
                        .ReturnsAsync(Result.Success());
            var controller = new TherapistController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateReview(validUpdateReviewDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsType<Result>(okResult.Value);
            Assert.True(resultValue.IsSuccess);
        }
    }
}
