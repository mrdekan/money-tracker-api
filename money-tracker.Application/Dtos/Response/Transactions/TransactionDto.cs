

using money_tracker.Domain.Entities;

namespace money_tracker.Application.Dtos.Response.Transactions
{
    public class TransactionDto
    {
        public TransactionDto(Transaction entity, string currency)
        {
            Id = entity.Id;
            Amount = entity.Amount;
            Currency = currency;
            Type = entity.Type.ToString();
            Comment = entity.Comment;
            CreatedAt = entity.CreatedAt;
        }

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
