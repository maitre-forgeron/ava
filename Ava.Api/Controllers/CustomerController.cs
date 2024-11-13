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
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto customer)
    {
        var result = await _mediator.Send(new AddCustomerCommand(customer));

        return CreatedAtAction(nameof(GetCustomerProfile), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerDto customer)
    {
        if (id != customer.Id) return BadRequest();
        await _mediator.Send(new UpdateCustomerCommand(customer));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/review")]
    public async Task<IActionResult> AddReviewToTherapist(Guid id, [FromBody] CreateReviewDto reviewDto)
    {
        var command = new AddReviewCommand(reviewDto.AuthorId, reviewDto.RecipientId, reviewDto.Rating, reviewDto.Summary);
        var reviewId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetCustomerProfile), new { id = reviewId }, reviewId);
    }
}
