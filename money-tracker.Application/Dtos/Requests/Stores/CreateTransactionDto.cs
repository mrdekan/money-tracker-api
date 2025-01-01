using System.ComponentModel.DataAnnotations;
using money_tracker.Domain.Enums;

namespace money_tracker.Application.Dtos.Requests.Stores
{
    public class CreateTransactionDto
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public TransactionTypes Type { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [StringLength(100, ErrorMessage = "The maximum length of a comment is 100 characters.")]
        public string Comment { get; set; } = string.Empty;
    }
}
