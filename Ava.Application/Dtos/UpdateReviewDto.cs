using System.ComponentModel.DataAnnotations;

namespace Ava.Application.Dtos;

public record UpdateReviewDto(
    [Required] Guid AuthorId,
    [Required] Guid RecipientId,
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")] int NewRating,
    [MaxLength(500, ErrorMessage = "Summary cannot exceed 500 characters.")] string NewSummary = ""
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
