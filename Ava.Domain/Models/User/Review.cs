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
        Rating = rating;
        Summary = summary;
    }
}
