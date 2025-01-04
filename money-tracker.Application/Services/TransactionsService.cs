using money_tracker.Application.Dtos.Requests.Transactions;
using money_tracker.Application.Dtos.Response.Transactions;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;
using money_tracker.Domain.Enums;
using money_tracker.Infrastructure.Repositories;
using money_tracker.Infrastructure.Services;

namespace money_tracker.Application.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly TransactionsRepository _transactionsRepository;
        private readonly JarsRepository _jarsRepository;
        private readonly StoresRepository _storeRepository;
        private readonly CurrencyBalancesRepository _currencyBalancesRepository;
        private readonly CurrenciesRepository _currenciesRepository;
        private readonly DbTransaction _dbTransaction;

        public TransactionsService(
            TransactionsRepository transactionsRepository,
            JarsRepository jarsRepository,
            CurrencyBalancesRepository currencyBalancesRepository,
            StoresRepository storeRepository,
            CurrenciesRepository currenciesRepository,
            DbTransaction dbTransaction
        )
        {
            _transactionsRepository = transactionsRepository;
            _jarsRepository = jarsRepository;
            _currencyBalancesRepository = currencyBalancesRepository;
            _storeRepository = storeRepository;
            _currenciesRepository = currenciesRepository;
            _dbTransaction = dbTransaction;
        }

        public async Task<ServiceResult> AddTransaction(CreateTransactionDto dto, int userId)
        {
            Jar? jar = await _jarsRepository.GetByStoreId(dto.StoreId);
            if (jar == null)
            {
                return ServiceResult.Fail("Jar not found", HttpCodes.NotFound);
            }
            if (jar.UserId != userId)
            {
                return ServiceResult.Fail(
                    $"You are not the owner of this jar",
                    HttpCodes.Forbidden
                );
            }

            Currency? currency = await _currenciesRepository.GetByNameAsync(dto.Currency);
            if (currency == null)
            {
                return ServiceResult.Fail($"Currency {dto.Currency} not found", HttpCodes.NotFound);
            }

            CurrencyBalance balance =
                await _currencyBalancesRepository.GetByStoreAndCurrencyIdAsync(
                    dto.StoreId,
                    currency.Id
                );

            if (dto.Type == TransactionTypes.Inflow)
            {
                balance.Balance += (decimal)dto.Amount;
            }
            else if (dto.Type == TransactionTypes.Outflow)
            {
                if (balance.Balance < (decimal)dto.Amount)
                {
                    return ServiceResult.Fail(
                        $"Balance ({balance.Balance}{jar.TargetCurrency.CC}) is less then {dto.Amount}{jar.TargetCurrency.CC}"
                    );
                }
                balance.Balance -= (decimal)dto.Amount;
            }

            Transaction transaction = new()
            {
                Type = dto.Type,
                Amount = (decimal)dto.Amount,
                Comment = dto.Comment,
                SourceId = balance.Id,
                UserId = userId,
            };

            await _dbTransaction.BeginAsync();
            try
            {
                await _currencyBalancesRepository.Update(balance);
                await _transactionsRepository.Add(transaction);
                await _dbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _dbTransaction.RollbackAsync();
                return ServiceResult.Fail(ex.Message, HttpCodes.InternalServerError);
            }
            return ServiceResult.Ok(new TransactionDto(transaction, currency.CC));
        }

        public async Task<ServiceResult> GetMany(TransactionQuery query, int userId)
        {
            if (query.StoreId.HasValue)
            {
                Store? store = await _storeRepository.GetByIdAsync(query.StoreId.Value);
                if (store == null)
                {
                    return ServiceResult.Fail(
                        $"Store with id {query.StoreId.Value} not found",
                        HttpCodes.NotFound
                    );
                }
            }

            long count = await _transactionsRepository.GetCount();

            var transactions = await _transactionsRepository.GetMany(
                query.Offset,
                query.Limit,
                userId,
                query.StoreId
            );

            var transactionsWithCurrency = transactions
                .Select(t => new TransactionDto(t, t.Source.Currency.CC))
                .ToList();

            return ServiceResult.Ok(
                new TransactionsListDto(
                    count,
                    query.Limit,
                    query.Offset + 1,
                    transactionsWithCurrency
                )
            );
        }
    }
}
