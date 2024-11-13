using System.ComponentModel.DataAnnotations;

namespace Ava.Application.Dtos;

public record UpdateReviewDto(
        [property: Required] Guid AuthorId,
        [property: Required] Guid RecipientId,
        [property: Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")] int NewRating,
        [property: MaxLength(500, ErrorMessage = "Summary cannot exceed 500 characters.")] string NewSummary
    )
{
    public IEnumerable<ValidationResult> Validate()
    {
        if (NewRating <= 3 && string.IsNullOrWhiteSpace(NewSummary))
        {
            yield return new ValidationResult("Summary is required for ratings 3 or lower.", [nameof(NewSummary)]);
        }
    }
}