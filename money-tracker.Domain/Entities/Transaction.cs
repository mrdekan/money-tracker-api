using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using money_tracker.Domain.Enums;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class Transaction : IDbStorable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public TransactionTypes Type { get; set; }

        [StringLength(100, ErrorMessage = "The maximum length of a comment is 100 characters.")]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        [Required(ErrorMessage = "UserId is required.")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "SourceId is required.")]
        [ForeignKey("Source")]
        public int SourceId { get; set; }
        public CurrencyBalance Source { get; set; }
    }
}
