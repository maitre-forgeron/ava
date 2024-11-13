using System.ComponentModel.DataAnnotations;

namespace Ava.Application.Dtos;

public record CreateReviewDto(
    [property: Required] Guid AuthorId,
    [property: Required] Guid RecipientId,
    [property: Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")] int Rating,
    [property: MaxLength(500, ErrorMessage = "Summary cannot exceed 500 characters.")] string Summary
)
{
    public IEnumerable<ValidationResult> Validate()
    {
        if (Rating <= 3 && string.IsNullOrWhiteSpace(Summary))
        {
            yield return new ValidationResult("Summary is required for ratings 3 or lower.", [nameof(Summary)]);
        }
    }
}
