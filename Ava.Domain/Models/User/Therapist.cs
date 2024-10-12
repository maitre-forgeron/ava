namespace Ava.Domain.Models.User
{
    public class Therapist : UserProfile
    {
        public Therapist(string firstName, string lastName, string personalId) : base(firstName, lastName, personalId)
        {
        }

        public Guid UserProfileId { get; init; }
        public decimal Rating { get; set; }
        public string Summary { get; set; }
        public Guid CertificateId { get; private set; }

        public UserProfile UserProfile { get; init; }

        public List<UserReview> Reviews { get; set; }
        public List<UserSpecialty> Specialties { get; set; }
    }
}
