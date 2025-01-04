using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Jar
{
    public class CreateJarDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [Length(2, 50, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required]
        [Length(3, 3, ErrorMessage = "Currency county code is 3 characters.")]
        public string TargetCurrency { get; set; }

        [Required(ErrorMessage = "Target is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Target is not negative")]
        public double Target { get; set; }
    }
}
