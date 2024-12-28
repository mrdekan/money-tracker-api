using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class CurrencyBalance : IDbStorable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance is not negative")]
        [DefaultValue(0)]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "StoreId is required.")]
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        [Required(ErrorMessage = "CurrencyId is required.")]
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
