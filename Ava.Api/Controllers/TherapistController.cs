using Ava.Application.Commands.Therapists;
using Ava.Application.Queries.Therapists;
using Ava.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers
{
    [ApiController]
    [Route("Therapist")]
    public class TherapistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TherapistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("alltherapists")]
        public async Task<IActionResult> GetAllTherapists()
        {
            var therapists = await _mediator.Send(new GetAllTherapistsQuery());
            if (therapists == null || !therapists.Any()) return NotFound();
            return Ok(therapists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTherapistProfile(Guid id)
        {
            var query = new GetTherapistProfileQuery { Id = id };
            var therapist = await _mediator.Send(query);
            if (therapist == null) return NotFound();
            return Ok(therapist);
        }

        [HttpPost]
        public async Task<IActionResult> AddTherapist([FromBody] TherapistDto therapistDto)
        {
            if (therapistDto == null) return BadRequest("Therapist data is required.");

            var result = await _mediator.Send(new AddTherapistCommand(therapistDto));
            return CreatedAtAction(nameof(GetTherapistProfile), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTherapist(Guid id, [FromBody] TherapistDto therapistDto)
        {
            if (therapistDto == null || id != therapistDto.Id) return BadRequest();

            await _mediator.Send(new UpdateTherapistCommand { Therapist = therapistDto });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTherapist(Guid id)
        {
            await _mediator.Send(new DeleteTherapistCommand { Id = id });
            return NoContent();
        }

        [HttpGet("{id}/reviews/top")]
        public async Task<IActionResult> GetTopReviews(Guid id)
        {
            var reviews = await _mediator.Send(new GetTopReviewsForTherapistQuery { TherapistId = id });
            return Ok(reviews);
        }

        [HttpGet("{id}/reviews/more")]
        public async Task<IActionResult> GetMoreReviews(Guid id, int skip, int take)
        {
            var reviews = await _mediator.Send(new GetMoreReviewsForTherapistQuery { TherapistId = id, Skip = skip, Take = take });
            return Ok(reviews);
        }
    }
}
