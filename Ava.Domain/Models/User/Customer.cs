namespace Ava.Domain.Models.User
{
    public class Customer : Entity
    {
        public Customer()
        {
        }

        public Guid UserProfileId { get; init; }

        public UserProfile UserProfile { get; init; }
    }
}
