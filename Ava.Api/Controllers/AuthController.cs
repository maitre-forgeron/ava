using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ava.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                return Ok();
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

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
