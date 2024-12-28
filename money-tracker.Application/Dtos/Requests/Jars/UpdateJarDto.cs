using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Jar
{
    public class UpdateJarDto
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string? Name { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "TargetCurrency is 3 characters.")]
        public string? TargetCurrency { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Target is not negative")]
        public decimal? Target { get; set; }
    }
}
