using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Auth
{
    public class SignInDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
