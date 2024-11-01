using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ava.Infrastructure.Services.Identity;

public interface IAuthService
{
    Task<LoginResult> Login(AuthDto model);
    Task<RegistrationResult> Register(AuthDto model);
}

public class AuthService : IAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<LoginResult> Login(AuthDto model)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.UserName.ToLower().Equals(model.UserName.ToLower()));

        if (user == null)
        {
            return new LoginResult(AuthResultStatus.NotFound);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!result.Succeeded)
        {
            await _userManager.AccessFailedAsync(user);

            return new LoginResult(AuthResultStatus.Unauthorized);
        }

        var token = GenerateToken(await GetClaims(user));

        return new LoginResult(AuthResultStatus.Ok, new TokenResult(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo));
    }

    public async Task<RegistrationResult> Register(AuthDto model)
    {
        var newUser = new User
        {
            UserName = model.UserName,
        };

        var result = await _userManager.CreateAsync(newUser, model.Password);

        var claimsResult = await AddClaims(newUser);

        if (result.Succeeded && claimsResult.Succeeded)
        {
            return new RegistrationResult(AuthResultStatus.Ok);
        }

        return new RegistrationResult(AuthResultStatus.BadRequest);
    }

    private JwtSecurityToken GenerateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var authClaims = await _userManager.GetClaimsAsync(user);

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            authClaims.Add(new Claim("Role", role));
        }

        return authClaims.ToList();
    }

    private async Task<IdentityResult> AddClaims(User newUser)
    {
        var claims = new List<Claim>
        {
            new Claim(CustomClaimType.Subject, newUser.Id.ToString()),
            new Claim(CustomClaimType.Name, newUser.UserName ?? string.Empty),
            new Claim(CustomClaimType.Email, newUser.Email ?? string.Empty),
        };

        var result = await _userManager.AddClaimsAsync(newUser, claims);

        return result;
    }
}
