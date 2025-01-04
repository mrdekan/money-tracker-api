using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Dtos.Response.Stores;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Repositories;

namespace money_tracker.Application.Services
{
    public class StoresService : IStoresService
    {
        private readonly StoresRepository _storesRepository;
        private readonly JarsRepository _jarsRepository;
        private readonly CurrencyBalancesRepository _currencyBalancesRepository;
        private readonly ICurrenciesService _currenciesService;

        public StoresService(
            StoresRepository storesRepository,
            JarsRepository jarsRepository,
            CurrencyBalancesRepository currencyBalancesRepository,
            ICurrenciesService currenciesService
        )
        {
            _storesRepository = storesRepository;
            _jarsRepository = jarsRepository;
            _currenciesService = currenciesService;
            _currencyBalancesRepository = currencyBalancesRepository;
        }

        public async Task<ServiceResult> AddStore(CreateStoreDto dto, int userId)
        {
            Jar jar = await _jarsRepository.GetByIdAsync(dto.JarId);
            if (jar == null)
            {
                return ServiceResult.Fail($"Jar with id {dto.JarId} not found", HttpCodes.NotFound);
            }

            if (jar.UserId != userId)
            {
                return ServiceResult.Fail(
                    $"You are not the owner of this jar",
                    HttpCodes.Forbidden
                );
            }

            Store store = new() { Name = dto.Name, JarId = dto.JarId };

            await _storesRepository.Add(store);

            return ServiceResult.Ok(new StoreDto(store));
        }

        public async Task<ServiceResult> UpdateStore(UpdateStoreDto dto, int userId, int storeId)
        {
            Store? store = await _storesRepository.GetByIdAsync(storeId);
            if (store == null)
            {
                return ServiceResult.Fail($"Store with id {storeId} not found", HttpCodes.NotFound);
            }

            Jar? jar = await _jarsRepository.GetByStoreId(storeId);
            if (jar.UserId != userId)
            {
                return ServiceResult.Fail(
                    $"You are not the owner of this jar",
                    HttpCodes.Forbidden
                );
            }

            store.Name = dto.Name;
            await _storesRepository.Update(store);
            return ServiceResult.Ok(await GetStoreWithBalance(store, jar.TargetCurrency.CC));
        }

        public async Task<ServiceResult> DeleteStore(int userId, int storeId)
        {
            Store? store = await _storesRepository.GetByIdAsync(storeId);
            if (store == null)
            {
                return ServiceResult.Fail($"Store with id {storeId} not found", HttpCodes.NotFound);
            }

            Jar? jar = await _jarsRepository.GetByStoreId(storeId);
            if (jar.UserId != userId)
            {
                return ServiceResult.Fail(
                    $"You are not the owner of this jar",
                    HttpCodes.Forbidden
                );
            }
            StoreDto res = await GetStoreWithBalance(store, jar.TargetCurrency.CC);
            await _storesRepository.Delete(store);
            return ServiceResult.Ok(res);
        }

        private async Task<StoreDto> GetStoreWithBalance(Store store, string targetCurrency)
        {
            Dictionary<string, decimal> currencyBalances = [];
            store.CurrencyBalances = (
                await _currencyBalancesRepository.GetByStoreIdAsync(store.Id)
            ).ToList();
            foreach (var balance in store.CurrencyBalances)
            {
                if (currencyBalances.Keys.Contains(balance.Currency.CC))
                {
                    currencyBalances[balance.Currency.CC] += balance.Balance;
                }
                else
                {
                    currencyBalances[balance.Currency.CC] = balance.Balance;
                }
            }
            return new StoreDto(store, _currenciesService, targetCurrency);
        }
    }
}
