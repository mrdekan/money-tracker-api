using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Dtos.Response.Transactions;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;
using money_tracker.Domain.Enums;
using money_tracker.Infrastructure.Repositories;

namespace money_tracker.Application.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly TransactionsRepository _transactionsRepository;
        private readonly JarsRepository _jarsRepository;
        private readonly CurrencyBalancesRepository _currencyBalancesRepository;

        public TransactionsService(TransactionsRepository transactionsRepository, JarsRepository jarsRepository, CurrencyBalancesRepository currencyBalancesRepository)
        {
            _transactionsRepository = transactionsRepository;
            _jarsRepository = jarsRepository;
            _currencyBalancesRepository = currencyBalancesRepository;
        }

        public async Task<ServiceResult> AddTransaction(CreateTransactionDto dto, int userId, int storeId)
        {
            Jar? jar = await _jarsRepository.GetByStoreId(storeId);
            if (jar == null)
            {
                return ServiceResult.Fail("Jar not found", HttpCodes.NotFound);
            }
            if (jar.UserId != userId)
            {
                return ServiceResult.Fail($"You are not the owner of this jar", HttpCodes.Forbidden);
            }

            CurrencyBalance balance = await _currencyBalancesRepository.GetByStoreAndCurrencyIdAsync(storeId, dto.CurrencyId);

            if (dto.Type == TransactionTypes.Inflow)
            {
                balance.Balance += (decimal)dto.Amount;
            }
            else if (dto.Type == TransactionTypes.Outflow)
            {
                if (balance.Balance < (decimal)dto.Amount)
                {
                    return ServiceResult.Fail($"Balance ({balance.Balance}{jar.TargetCurrency.CC}) is less then {dto.Amount}{jar.TargetCurrency.CC}");
                }
                balance.Balance -= (decimal)dto.Amount;
            }

            await _currencyBalancesRepository.Update(balance);

            Transaction transaction = new Transaction()
            {
                Type = dto.Type,
                Amount = (decimal)dto.Amount,
                Comment = dto.Comment,
                SourceId = balance.Id,
                UserId = userId,
            };
            await _transactionsRepository.Add(transaction);

            return ServiceResult.Ok(new TransactionDto(transaction, jar.TargetCurrency.CC));
        }
    }
}
