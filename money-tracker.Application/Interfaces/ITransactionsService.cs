using money_tracker.Application.Dtos.Requests.Transactions;
using money_tracker.Application.Services;

namespace money_tracker.Application.Interfaces
{
    public interface ITransactionsService
    {
        public Task<ServiceResult> AddTransaction(CreateTransactionDto dto, int userId);
        public Task<ServiceResult> GetMany(TransactionQuery query, int userId);
    }
}
