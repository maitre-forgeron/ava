using Ava.Application.Dtos;
using Ava.Application.Therapists.Commands;
using Ava.Application.Therapists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TherapistController : ControllerBase
{
    private readonly IMediator _mediator;

    public TherapistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("alltherapists")]
    public async Task<IActionResult> GetAllTherapists(Guid? CategoryId, int skip, int take)
    {
        var therapists = await _mediator.Send(new GetAllTherapistsQuery(CategoryId, skip, take));

        if (therapists == null || !therapists.Any())
        {
            return NotFound();
        }

        return Ok(therapists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTherapistProfile(Guid id)
    {
        var query = new GetTherapistProfileQuery(id);

        var therapist = await _mediator.Send(query);

        if (therapist == null)
        {
            return NotFound();
        }

        return Ok(therapist);
    }

    [HttpPost]
    public async Task<IActionResult> AddTherapist([FromBody] CreateTherapistDto therapistDto)
    {
        if (therapistDto == null)
        {
            return BadRequest("Therapist data is required.");
        }

        var result = await _mediator.Send(new AddTherapistCommand(therapistDto));

        return CreatedAtAction(nameof(GetTherapistProfile), new { id = result }, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTherapist([FromBody] UpdateTherapistDto therapistDto)
    {
        if (therapistDto == null || therapistDto.Id != therapistDto.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(new UpdateTherapistCommand(therapistDto));

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTherapist(Guid id)
    {
        await _mediator.Send(new DeleteTherapistCommand(id));

        return NoContent();
    }

    [HttpGet("{id}/reviews/more")]
    public async Task<IActionResult> GetMoreReviews(Guid id, int skip, int take)
    {
        var reviews = await _mediator.Send(new GetMoreReviewsForTherapistQuery(id, skip, take));

        return Ok(reviews);
    }
}
