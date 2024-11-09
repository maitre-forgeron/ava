using Ava.Application.Customers.Commands;
using Ava.Application.Customers.Queries;
using Ava.Application.Dtos;
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
}
