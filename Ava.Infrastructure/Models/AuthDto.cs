using System.ComponentModel.DataAnnotations;

namespace Ava.Infrastructure.Models
{
    public class AuthDto
    {
        [Required(ErrorMessage = "Username is required")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
