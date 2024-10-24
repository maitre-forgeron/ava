namespace Ava.Domain.Models.User
{
    public class Therapist : Entity
    {
        public Therapist()
        {
        }

        public Guid UserProfileId { get; init; }
        public decimal Rating { get; set; }
        public string Summary { get; set; }
        public Guid CertificateId { get; private set; }

        public UserProfile UserProfile { get; init; }

        public List<Review> Reviews { get; set; }
    }
}
