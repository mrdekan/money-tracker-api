namespace money_tracker.Application.Dtos.Requests.Transactions
{
    public class TransactionQuery : PaginatedRequest
    {
        public int? StoreId { get; set; }
    }
}
