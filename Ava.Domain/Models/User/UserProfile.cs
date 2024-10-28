namespace Ava.Domain.Models.User;

public abstract class UserProfile : AggregateRoot
{
    public string UserName { get; private set; }

    public string Email { get; private set; }

    public string Phone { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PersonalId { get; private set; }

    public Guid? PhotoId { get; private set; }

    private List<Review> _authorReviews;
    public IReadOnlyCollection<Review> AuthorReviews => _authorReviews.AsReadOnly();

    public List<Review> _recipientReviews;
    public IReadOnlyCollection<Review> RecipientReviews => _recipientReviews.AsReadOnly();

    protected UserProfile()
    {
    }

    public UserProfile(Guid userId, string userName, string email, string phone, string firstName, string lastName, string personalId) : base(userId)
    {
        Id = userId;
        UserName = userName;
        Email = email;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        PersonalId = personalId;
    }

    public void SetPhoto(Guid photoId)
    {
        PhotoId = photoId;
    }
}
