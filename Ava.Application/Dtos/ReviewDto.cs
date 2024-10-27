namespace Ava.Application.Dtos
{
    public record ReviewDto
    {
        public Guid SenderId { get; init; }
        public Guid RecipientId { get; init; }
        public int ReviewValue { get; init; }
        public string ReviewText { get; init; }
    }
}
