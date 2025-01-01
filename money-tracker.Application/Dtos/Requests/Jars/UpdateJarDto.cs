using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Jar
{
    public class UpdateJarDto
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string? Name { get; set; }

        public int? TargetCurrencyId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Target is not negative")]
        public double? Target { get; set; }
    }
}
