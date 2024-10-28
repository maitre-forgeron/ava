namespace Ava.Domain.Models.User;

public abstract class UserProfile : AggregateRoot
{
    public string FirstName { get; protected set; }

    public string LastName { get; protected set; }

    public string PersonalId { get; protected set; }

    public Guid? PhotoId { get; protected set; }

    private List<Review> _authorReviews;
    public IReadOnlyCollection<Review> AuthorReviews => _authorReviews?.AsReadOnly();

    public List<Review> _recipientReviews;
    public IReadOnlyCollection<Review> RecipientReviews => _recipientReviews?.AsReadOnly();

    protected UserProfile()
    {
    }

    public UserProfile(Guid userId, string firstName, string lastName, string personalId) : base(userId)
    {
        Id = userId;
        FirstName = firstName;
        LastName = lastName;
        PersonalId = personalId;
    }

    public void SetPhoto(Guid photoId)
    {
        PhotoId = photoId;
    }
}
