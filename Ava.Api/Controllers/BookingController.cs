using Ava.Application.Bookings.Commands;
using Ava.Application.Bookings.Queries;
using Ava.Application.Dtos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var bookingDto = await _mediator.Send(new GetBookingQuery(id));
            if (bookingDto == null) return NotFound();

            return Ok(bookingDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] CreateBookingDto booking)
        {
            var result = await _mediator.Send(new AddBookingCommand(booking));

            return CreatedAtAction(nameof(GetBooking), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> ApproveBooking([FromBody] BookingActionDto action)
        {
            await _mediator.Send(new ApproveBookingCommand(action));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> RejectBooking([FromBody] BookingActionDto action)
        {
            await _mediator.Send(new RejectBookingCommand(action));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            await _mediator.Send(new DeleteBookingCommand(id));

            return NoContent();
        }
    }
}
