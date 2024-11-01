namespace Ava.Infrastructure.Models;

public enum AuthResultStatus
{
    NotFound,
    Unauthorized,
    Ok,
    BadRequest
}

public record TokenResult(string Token, DateTime ValidTo);

public record LoginResult(AuthResultStatus Status, TokenResult? TokenResult = null);

public record RegistrationResult(AuthResultStatus Status);