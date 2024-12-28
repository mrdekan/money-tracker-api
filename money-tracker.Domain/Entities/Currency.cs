using System.ComponentModel.DataAnnotations;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class Currency : IDbStorable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency is 3 characters.")]
        public string CC { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Can not be negative")]
        public double Rate { get; set; }

        public ICollection<CurrencyBalance> Balances { get; set; } = new List<CurrencyBalance>();
        public ICollection<Jar> TargetedJars { get; set; } = new List<Jar>();
    }
}
