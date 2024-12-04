using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class CurrencyBalance : IDbStorable
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency is 3 characters.")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        public decimal Balance { get; set; }


        [Required(ErrorMessage = "UserId is required.")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "StoreId is required.")]
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public Store Store { get; set; }


        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
