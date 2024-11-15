using Ava.Api.Extensions;
using Ava.Infrastructure.Models;
using Ava.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] AuthDto model)
    {
        var loginResult = await _authService.Login(model);

        return loginResult.ToResult();
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] AuthDto model)
    {
        var registrationResult = await _authService.Register(model);

        return registrationResult.ToResult();
    }
}
