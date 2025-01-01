using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Auth
{
    public class SignUpDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(50, ErrorMessage = "Email min length is 50 characters")]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password min length is 8 characters")]
        [MaxLength(50, ErrorMessage = "Password min length is 50 characters")]
        public string Password { get; set; }
    }
}
