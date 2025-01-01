using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class Jar : IDbStorable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Target is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Target is not negative")]
        public decimal Target { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        [Required(ErrorMessage = "TargetCurrencyId is required.")]
        [ForeignKey("TargetCurrency")]
        public int TargetCurrencyId { get; set; }
        public Currency TargetCurrency { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
