namespace Ava.Application.Dtos
{
    public record TherapistDto
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; init; }
        public decimal Rating { get; init; }
        public string Summary { get; init; }
        public Guid CertificateId { get; init; }
        public List<ReviewDto> Reviews { get; init; } = new();
    }
}
