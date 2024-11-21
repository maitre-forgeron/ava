using Ava.Application.Customers.Commands;
using Ava.Application.Customers.Queries;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Application.Reviews.Commands;
using Ava.Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("allcustomers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _mediator.Send(new GetAllCustomersQuery());

        if (customers == null || !customers.Any())
        {
            return NotFound();
        }

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerProfile(Guid id)
    {
        var customer = await _mediator.Send(new GetCustomerProfileQuery(id));

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto customer)
    {
        if (customer == null)
        {
            return BadRequest(new Error("400", "Invalid customer data"));
        }

        var result = await _mediator.Send(new AddCustomerCommand(customer));

        if (result.IsFailure)
        {
            return BadRequest(new Error("400", result.Error.Description));
        }

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto customer)
    {
        var result = await _mediator.Send(new UpdateCustomerCommand(customer));

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var result = await _mediator.Send(new DeleteCustomerCommand(id));

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpPost("review/add")]
    public async Task<IActionResult> AddReviewToTherapist([FromBody] CreateReviewDto reviewDto)
    {
        var command = new AddReviewCommand(reviewDto.AuthorId, reviewDto.RecipientId, reviewDto.Rating, reviewDto.Summary);
        var reviewId = await _mediator.Send(command);

        if (reviewId == null || reviewId.IsFailure)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetCustomerProfile), new { id = reviewId }, reviewId);
    }

    [HttpPut("review/update")]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDto updateReviewDto)
    {
        var result = await _mediator.Send(new UpdateReviewCommand(
            updateReviewDto.AuthorId,
            updateReviewDto.RecipientId,
            updateReviewDto.NewRating,
            updateReviewDto.NewSummary
        ));

        if (result == null || result.IsFailure)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
