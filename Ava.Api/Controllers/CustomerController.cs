using Ava.Domain.Models.User;
using Ava.Infrastructure.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers
{
    [ApiController]
    [Route("Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("allcustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            if (customers == null) return NotFound();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerProfile(Guid id)
        {
            var customer = await _customerService.GetCustomerProfileAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerProfile), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            if (id != customer.Id) return BadRequest();
            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
