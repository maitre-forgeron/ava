namespace Ava.Domain.Models.User
{
    public class Review : Entity
    {
        /// <summary>
        /// UserProfileId
        /// </summary>
        public Guid SenderId { get; init; }
        /// <summary>
        /// UserProfileId
        /// </summary>
        public Guid RecipientId { get; init; }
        public int ReviewValue { get; set; }
        public string ReviewText { get; set; }

        public UserProfile Sender { get; set; }
        public UserProfile Recipient { get; set; }
    }
}
