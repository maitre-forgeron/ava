using Ava.Domain.Models.User;
using Ava.Infrastructure.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers
{
    [ApiController]
    [Route("Therapist")]
    public class TherapistController : ControllerBase
    {
        private readonly ITherapistService _therapistService;

        public TherapistController(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        [HttpGet("alltherapists")]
        public async Task<IActionResult> GetAllTherapists()
        {
            var therapists = await _therapistService.GetAllTherapistsAsync();
            if (therapists == null) return NotFound();
            return Ok(therapists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTherapistProfile(Guid id)
        {
            var therapist = await _therapistService.GetTherapistProfileAsync(id);
            if (therapist == null) return NotFound();
            return Ok(therapist);
        }

        [HttpPost]
        public async Task<IActionResult> AddTherapist([FromBody] Therapist therapist)
        {
            await _therapistService.AddTherapistAsync(therapist);
            return CreatedAtAction(nameof(GetTherapistProfile), new { id = therapist.Id }, therapist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTherapist(Guid id, [FromBody] Therapist therapist)
        {
            if (id != therapist.Id) return BadRequest();
            await _therapistService.UpdateTherapistAsync(therapist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTherapist(Guid id)
        {
            await _therapistService.DeleteTherapistAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/reviews/top")]
        public async Task<IActionResult> GetTopReviews(Guid id)
        {
            var reviews = await _therapistService.GetTopReviewsForTherapistAsync(id);
            return Ok(reviews);
        }

        [HttpGet("{id}/reviews/more")]
        public async Task<IActionResult> GetMoreReviews(Guid id, int skip, int take)
        {
            var reviews = await _therapistService.GetMoreReviewsForTherapistAsync(id, skip, take);
            return Ok(reviews);
        }
    }
}
