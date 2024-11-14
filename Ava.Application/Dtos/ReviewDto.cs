namespace Ava.Application.Dtos;

public record CreateReviewDto(Guid AuthorId, Guid RecipientId, int Rating, string Summary = "");
public record UpdateReviewDto(Guid AuthorId, Guid RecipientId, int NewRating, string NewSummary = "");
