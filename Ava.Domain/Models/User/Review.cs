namespace Ava.Domain.Models.User
{
    public class Review : Entity
    {
        public Guid SenderId { get; init; }
        public Guid RecipientId { get; init; }
        public int ReviewValue { get; set; }
        public string ReviewText { get; set; }

        public UserProfile Sender { get; init; }
        public UserProfile Recipient { get; init; }
    }
}
