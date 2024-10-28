using Ava.Application.Customers.Commands;
using Ava.Application.Customers.Queries;
using Ava.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
