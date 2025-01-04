namespace money_tracker.Application.Dtos.Response.Transactions
{
    public class TransactionsListDto : PaginatedResponse
    {
        public TransactionsListDto(
            long total,
            int perPage,
            int page,
            List<TransactionDto> transactions
        )
            : base(total, perPage, page)
        {
            Transactions = transactions;
        }

        public List<TransactionDto> Transactions { get; set; }
    }
}
