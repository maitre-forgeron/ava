namespace Ava.Domain.Models.User
{
    public class Customer : Entity
    {
        public Guid UserProfileId { get; init; }

        public UserProfile UserProfile { get; init; }
    }
}
