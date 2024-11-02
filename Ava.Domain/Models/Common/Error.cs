namespace Ava.Domain.Models.Common;

public sealed record Error(string code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}
