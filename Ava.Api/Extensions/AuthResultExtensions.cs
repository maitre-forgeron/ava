using Ava.Infrastructure.Models;

namespace Ava.Api.Extensions;

public static class AuthResultExtensions
{
    public static IResult ToResult(this LoginResult loginResult)
    {
        var result = loginResult.Status switch
        {
            AuthResultStatus.NotFound => Results.NotFound(),
            AuthResultStatus.Unauthorized => Results.Unauthorized(),
            AuthResultStatus.Ok => Results.Ok(loginResult.TokenResult),
            _ => Results.BadRequest()
        };

        return result;
    }

    public static IResult ToResult(this RegistrationResult registrationResult)
    {
        var result = registrationResult.Status switch
        {
            AuthResultStatus.Ok => Results.Ok(),
            _ => Results.BadRequest()
        };

        return result;
    }
}
