using System.ComponentModel.DataAnnotations;
using money_tracker.Domain.Enums;

namespace money_tracker.Application.Dtos.Requests.Transactions
{
    public class CreateTransactionDto
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        public TransactionTypes Type { get; set; }

        [Required]
        [Length(3, 3, ErrorMessage = "Currency county code is 3 characters.")]
        public string Currency { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The maximum length of a comment is 100 characters.")]
        public string Comment { get; set; }
    }
}
