namespace Ava.Domain.Models.User;

public class Therapist : UserProfile
{
    public double Rating { get; private set; }

    public string Summary { get; private set; }

    public Guid CertificateId { get; private set; }

    private Therapist()
    {
    }

    public Therapist(Guid userId, string userName, string email, string phone, string firstName, string lastName, string personalId, double rating, string summary, Guid certificateId)
        : base(userId, userName, email, phone, firstName, lastName, personalId)
    {
        Rating = rating;
        Summary = summary;
        CertificateId = certificateId;
    }
}
