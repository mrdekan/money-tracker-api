using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Jar
{
    public class CreateJarDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required]
        public int TargetCurrencyId { get; set; }

        [Required(ErrorMessage = "Target is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Target is not negative")]
        public double Target { get; set; }
    }
}
