namespace Ava.Application.Dtos
{
    public record CustomerDto
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; init; }
    }
}
