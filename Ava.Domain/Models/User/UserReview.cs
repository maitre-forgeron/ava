namespace Ava.Domain.Models.User
{
    public class UserReview : Entity
    {
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
    }
}