namespace Ava.Application.Dtos;

public record ReviewDto
{
    public Guid AuthorId { get; init; }
    public Guid RecipientId { get; init; }
    public int Rating { get; init; }
    public string Summary { get; init; }
}
