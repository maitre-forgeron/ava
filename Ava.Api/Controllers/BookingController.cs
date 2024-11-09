﻿using Ava.Application.Bookings.Commands;
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

            if (bookingDto == null)
            {
                return NotFound();
            }

            return Ok(bookingDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] CreateBookingDto booking)
        {
            var result = await _mediator.Send(new AddBookingCommand(booking));

            return Ok();
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveBooking(Guid id)
        {
            var result = await _mediator.Send(new ApproveBookingCommand(id));

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectBooking(Guid id)
        {
            var result = await _mediator.Send(new RejectBookingCommand(id));

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var result = await _mediator.Send(new DeleteBookingCommand(id));

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
