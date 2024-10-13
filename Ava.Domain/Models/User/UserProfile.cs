namespace Ava.Domain.Models.User
{
    public class UserProfile : Entity
    {
        public UserProfile(string firstName, string lastName, string personalId)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonalId = personalId;
        }

        /// <summary>
        /// UserId
        /// </summary>
        public string SubjectId { get; init; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PersonalId { get; private set; }
        public Guid Photo { get; private set; }

        public void SetPhoto(Guid photo)
        {
            Photo = photo;
        }

        public List<Review> SenderReviews { get; set; }
        public List<Review> RecipientReviews { get; set; }
    }
}
