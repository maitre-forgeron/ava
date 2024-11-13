using Ava.Api.Controllers;
using Ava.Application.Customers.Commands;
using Ava.Application.Customers.Queries;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Application.Reviews.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Ava.Tests.Unit.Controllers
{
    [Collection("Customer_Collection")]
    public class CustomerControllerShould
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomerControllerShould(ServiceProviderFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async Task ReturnAllCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto>
            {
                new CustomerDto(Guid.NewGuid(), "John", "Doe", "12345", null),
                new CustomerDto(Guid.NewGuid(), "Jane", "Smith", "67890", null)
            };
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCustomersQuery>(), default))
                         .ReturnsAsync(customers);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.GetAllCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomers = Assert.IsType<List<CustomerDto>>(okResult.Value);
            Assert.Equal(customers.Count, returnedCustomers.Count);
        }

        [Fact]
        public async Task ReturnNotFoundWhenNoCustomersExist()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCustomersQuery>(), default))
                         .ReturnsAsync(new List<CustomerDto>());
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.GetAllCustomers();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task ReturnCustomerProfileWhenCustomerExists()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CustomerDto(customerId, "John", "Doe", "12345", null);
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerProfileQuery>(), default))
                         .ReturnsAsync(customer);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.GetCustomerProfile(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomer = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal(customer.Id, returnedCustomer.Id);
        }

        [Fact]
        public async Task ReturnNotFoundWhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerProfileQuery>(), default))
                         .ReturnsAsync((CustomerDto)null);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.GetCustomerProfile(customerId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestWhenAddingInvalidCustomer()
        {
            // Arrange
            CreateCustomerDto invalidCustomer = null;
            var failureResult = Result.Failure(new Error("400", "Invalid customer data"));
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddCustomerCommand>(), default))
                         .ReturnsAsync(failureResult);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.AddCustomer(invalidCustomer);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<Error>(badRequestResult.Value);
            Assert.Equal("Invalid customer data", error.Description);
        }

        [Fact]
        public async Task ReturnOkWhenAddingCustomerSuccessfully()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CreateCustomerDto(customerId, "John", "Doe", "12345");
            var expectedResult = Result.Success();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddCustomerCommand>(), default))
                         .ReturnsAsync(expectedResult);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.AddCustomer(customer);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenUpdatingInvalidCustomer()
        {
            // Arrange
            var invalidCustomer = new UpdateCustomerDto(Guid.NewGuid(), "John", "Doe");
            var failureResult = Result.Failure(new Error("400", "Invalid customer data"));
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCustomerCommand>(), default))
                         .ReturnsAsync(failureResult);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateCustomer(invalidCustomer);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<Error>(badRequestResult.Value);
            Assert.Equal("Invalid customer data", error.Description);
        }

        [Fact]
        public async Task ReturnOkWhenCustomerUpdatedSuccessfully()
        {
            // Arrange
            var customer = new UpdateCustomerDto(Guid.NewGuid(), "John", "Doe");
            var successResult = Result.Success();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCustomerCommand>(), default))
                         .ReturnsAsync(successResult);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateCustomer(customer);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task ReturnNoContentWhenCustomerDeletedSuccessfully()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var successResult = Result.Success();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCustomerCommand>(), default))
                         .ReturnsAsync(successResult);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.DeleteCustomer(customerId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenDeletingInvalidCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var failureResult = Result.Failure(new Error("400", "Customer not found"));
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCustomerCommand>(), default))
                         .ReturnsAsync(failureResult);
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.DeleteCustomer(customerId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<Error>(badRequestResult.Value);
            Assert.Equal("Customer not found", error.Description);
        }

        [Fact]
        public async Task ReturnCreatedAtActionWhenAddingReviewSuccessfully()
        {
            // Arrange
            var reviewDto = new CreateReviewDto(Guid.NewGuid(), Guid.NewGuid(), 5, "Great service");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddReviewCommand>(), default))
                         .ReturnsAsync(Result.Success());
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.AddReviewToTherapist(reviewDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetCustomerProfile", createdResult.ActionName);
        }

        [Fact]
        public async Task ReturnBadRequestWhenAddingInvalidReview()
        {
            // Arrange
            var invalidReviewDto = new CreateReviewDto(Guid.Empty, Guid.Empty, 0, "");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddReviewCommand>(), default))
                         .ReturnsAsync(Result.Success());
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.AddReviewToTherapist(invalidReviewDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenReviewUpdatedSuccessfully()
        {
            // Arrange
            var updateReviewDto = new UpdateReviewDto(Guid.NewGuid(), Guid.NewGuid(), 4, "Good service");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateReviewCommand>(), default))
                         .ReturnsAsync(Result.Success());
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateReview(updateReviewDto);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenReviewDtoFailsValidation()
        {
            // Arragne
            var invalidReviewDto = new CreateReviewDto(Guid.Empty, Guid.Empty, 0, "");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddReviewCommand>(), default))
                        .ReturnsAsync(Result.Failure(new Error("400", "Invalid review data")));
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.AddReviewToTherapist(invalidReviewDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            var validationErrors = badRequestResult.Value as List<ValidationResult>;
            Assert.NotNull(validationErrors);
            Assert.Contains(validationErrors, v => v.MemberNames.Contains("Summary") && v.ErrorMessage.Contains("Summary is required for ratings 3 or lower."));

        }

        [Fact]
        public async Task ReturnBadRequestWhenUpdatingReviewFailsValidation()
        {
            // Arrange
            var invalidUpdateReviewDto = new UpdateReviewDto(Guid.Empty, Guid.Empty, 0, "");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateReviewCommand>(), default))
                        .ReturnsAsync(Result.Failure(new Error("400", "Invalid review data")));
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateReview(invalidUpdateReviewDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            var validationErrors = badRequestResult.Value as List<ValidationResult>;
            Assert.NotNull(validationErrors);
            Assert.Contains(validationErrors, v => v.MemberNames.Contains("NewSummary") && v.ErrorMessage.Contains("Summary is required for ratings 3 or lower."));
        }

        [Fact]
        public async Task ReturnBadRequestWhenSummaryIsRequiredForLowRating()
        {
            // Arrange
            var invalidReviewDto = new CreateReviewDto(Guid.NewGuid(), Guid.NewGuid(), 2, "");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<AddReviewCommand>(), default))
                        .ReturnsAsync(Result.Failure(new Error("400", "Invalid review data")));
            var controller = new CustomerController(mediatorMock.Object);

            // Act
            var result = await controller.AddReviewToTherapist(invalidReviewDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            var validationErrors = badRequestResult.Value as List<ValidationResult>;
            Assert.NotNull(validationErrors);
            Assert.Contains(validationErrors, v => v.MemberNames.Contains("Summary") && v.ErrorMessage.Contains("Summary is required for ratings 3 or lower."));
        }
    }
}
