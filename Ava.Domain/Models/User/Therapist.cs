namespace Ava.Domain.Models.User;

public class Therapist : UserProfile
{
    public double Rating { get; private set; }

    public string Summary { get; private set; }

    public Guid CertificateId { get; private set; }

    private Therapist()
    {
    }

    public Therapist(Guid userId, string firstName, string lastName, string personalId, double rating, string summary, Guid certificateId)
        : base(userId, firstName, lastName, personalId)
    {
        Rating = rating;
        Summary = summary;
        CertificateId = certificateId;
    }

    public void Update(string firstName, string lastName, double rating, string summary)
    {
        FirstName = firstName;
        LastName = lastName;
        Rating = rating;
        Summary = summary;
    }

    public void UpdateRating(double newRating)
    {
        Rating = newRating;
    }
}
