using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Application.Reviews.Commands;
using Ava.Application.Reviews.Queries;
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
    public async Task<IActionResult> GetAllTherapists()
    {
        var therapists = await _mediator.Send(new GetAllCustomersQuery());

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

        var result = await _mediator.Send(new UpdateTherapistCommand(therapistDto));

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTherapist(Guid id)
    {
        await _mediator.Send(new DeleteTherapistCommand(id));

        return NoContent();
    }

    [HttpGet("{id}/review/summary")]
    public async Task<IActionResult> GetRatingSummary(Guid id, CancellationToken cancellationToken)
    {
        var ratingSummary = await _mediator.Send(new GetRatingSummaryQuery(id), cancellationToken);

        return Ok(ratingSummary);
    }

    [HttpGet("{id}/review/all")]
    public async Task<IActionResult> GetAllReviews(Guid id, int skip, int take)
    {
        var reviews = await _mediator.Send(new GetMoreReviewsForTherapistQuery(id, skip, take ));

        return Ok(reviews);
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

        return CreatedAtAction(nameof(GetTherapistProfile), new { id = reviewId }, reviewId);
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
