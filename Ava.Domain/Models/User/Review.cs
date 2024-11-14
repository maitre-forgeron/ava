using System.ComponentModel.DataAnnotations;

namespace Ava.Domain.Models.User;

public class Review : Entity
{
    public Guid AuthorId { get; private set; }

    public Guid RecipientId { get; private set; }

    public int Rating { get; private set; }

    public string Summary { get; private set; }

    public UserProfile Author { get; private set; }

    public UserProfile Recipient { get; private set; }

    private Review()
    {
    }

    public Review(Guid id, Guid authorId, Guid recipientId, int rating, string summary) : base(id)
    {
        AuthorId = authorId;
        RecipientId = recipientId;
        ValidateRating(rating, summary);
        Rating = rating;
        Summary = summary;
    }

    public void Update(int newRating, string newSummary)
    {
        ValidateRating(newRating, newSummary);
        Rating = newRating;
        Summary = newSummary;
    }

    private static void ValidateRating(int rating, string summary)
    {
        if (rating < 1 || rating > 5)
        {
            throw new ValidationException("Rating must be between 1 and 5.");
        }

        if (rating <= 3 && string.IsNullOrWhiteSpace(summary))
        {
            throw new ValidationException("Summary is required for ratings 3 or lower.");
        }

        if (summary?.Length > 500)
        {
            throw new ValidationException("Summary cannot exceed 500 characters.");
        }
    }
}
