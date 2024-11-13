using Ava.Application.Customers.Commands;
using Ava.Application.Customers.Queries;
using Ava.Application.Dtos;
using Ava.Application.Reviews.Commands;
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
        var result = await _mediator.Send(new AddCustomerCommand(customer));

        if (customer == null)
        {
            return NotFound();
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

    [HttpPost("{id}/review")]
    public async Task<IActionResult> AddReviewToTherapist(Guid id, [FromBody] CreateReviewDto reviewDto)
    {
        var validationResults = reviewDto.Validate().ToList();
        if (validationResults.Any())
        {
            return BadRequest(validationResults);
        }

        var command = new AddReviewCommand(reviewDto.AuthorId, reviewDto.RecipientId, reviewDto.Rating, reviewDto.Summary);
        var reviewId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetCustomerProfile), new { id = reviewId }, reviewId);
    }

    [HttpPut("reviews/update")]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDto updateReviewDto)
    {
        var validationResults = updateReviewDto.Validate().ToList();
        if (validationResults.Any())
        {
            return BadRequest(validationResults);
        }

        var result = await _mediator.Send(new UpdateReviewCommand(
            updateReviewDto.AuthorId,
            updateReviewDto.RecipientId,
            updateReviewDto.NewRating,
            updateReviewDto.NewSummary
        ));

        return Ok(result);
    }
}
