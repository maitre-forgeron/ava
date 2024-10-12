namespace Ava.Domain.Models.User
{
    public class Customer : UserProfile
    {
        public Customer(string firstName, string lastName, string personalId) : base(firstName, lastName, personalId)
        {
        }

        public Guid UserProfileId { get; init; }

        public UserProfile UserProfile { get; init; }
    }
}
