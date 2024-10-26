using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ava.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] AuthDto model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName.ToLower().Equals(model.UserName.ToLower()));

            if (user == null)
            {
                return NotFound();
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                var token = GenerateToken(await GetClaims(user));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else
            {
                await _userManager.AccessFailedAsync(user);

                return Unauthorized();
            }
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] AuthDto model)
        {
            var newUser = new User
            {
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            var claimsResult = await AddClaims(newUser);

            // Add UserProfile Creation

            if (result.Succeeded && claimsResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
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

        private async Task<IdentityResult> AddClaims(User newUser)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimType.Subject, newUser.Id),
                new Claim(CustomClaimType.Name, newUser.UserName ?? string.Empty),
                new Claim(CustomClaimType.Email, newUser.Email ?? string.Empty),
                //new Claim(CustomClaimType.Role, newUser)
                //new Claim(CustomClaimType.Scopes, )
                //new Claim(CustomClaimType.FamilyName, newUser.)
            };

            var result = await _userManager.AddClaimsAsync(newUser, claims);

            return result;
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
    }
}
