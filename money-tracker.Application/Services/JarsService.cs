using money_tracker.Application.Dtos.Requests.Jar;
using money_tracker.Application.Dtos.Response.Jars;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Repositories;

namespace money_tracker.Application.Services
{
    public class JarsService : IJarsService
    {
        private readonly JarsRepository _jarsRepository;
        private readonly CurrenciesRepository _currenciesRepository;
        private readonly CurrencyBalancesRepository _currencyBalancesRepository;
        private readonly StoresRepository _stocksRepository;
        private readonly ICurrenciesService _currenciesService;

        public JarsService(
            JarsRepository jarsRepository,
            CurrenciesRepository currenciesRepository,
            CurrencyBalancesRepository currencyBalancesRepository,
            ICurrenciesService currencyService
        )
        {
            _jarsRepository = jarsRepository;
            _currenciesRepository = currenciesRepository;
            _currencyBalancesRepository = currencyBalancesRepository;
            _currenciesService = currencyService;
        }

        public async Task<ServiceResult> AddJar(CreateJarDto dto, int userId)
        {
            Currency? currency = await _currenciesRepository.GetByNameAsync(dto.TargetCurrency);
            if (currency == null)
            {
                return ServiceResult.Fail(
                    $"Currency {dto.TargetCurrency} not found",
                    HttpCodes.NotFound
                );
            }

            bool nameTaken = await _jarsRepository.IsNameTaken(dto.Name, userId);
            if (nameTaken)
            {
                return ServiceResult.Fail($"Name {dto.Name} is already taken", HttpCodes.Conflict);
            }

            Jar jar = new()
            {
                Name = dto.Name,
                Target = (decimal)dto.Target,
                TargetCurrencyId = currency.Id,
                UserId = userId,
            };

            await _jarsRepository.Add(jar);

            return ServiceResult.Ok(new JarDto(jar));
        }

        public async Task<ServiceResult> GetJar(int userId, int jarId)
        {
            Jar? jar = await _jarsRepository.GetByIdAsync(jarId);
            if (jar == null)
            {
                return ServiceResult.Fail($"Jar with id {jarId} not found", HttpCodes.NotFound);
            }

            if (jar.UserId != userId)
            {
                return ServiceResult.Fail(
                    $"You are not the owner of this jar",
                    HttpCodes.Forbidden
                );
            }

            DetailedJarDto dto = await GetJarWithBalance(jar);

            return ServiceResult.Ok(dto);
        }

        public async Task<ServiceResult> GetJars(int userId)
        {
            var jars = await _jarsRepository.GetByUserId(userId);
            List<JarDto> result = [];
            foreach (var jar in jars)
            {
                JarDto dto = await GetJarWithBalance(jar);
                result.Add(dto);
            }

            return ServiceResult.Ok(result);
        }

        public async Task<ServiceResult> UpdateJar(UpdateJarDto dto, int userId, int jarId)
        {
            Jar? jar = await _jarsRepository.GetByIdAsync(jarId);

            if (jar == null)
            {
                return ServiceResult.Fail($"Jar with id {jarId} not found", HttpCodes.NotFound);
            }

            if (jar.UserId != userId)
            {
                return ServiceResult.Fail(
                    $"You are not the owner of this jar",
                    HttpCodes.Forbidden
                );
            }

            if (dto.TargetCurrencyId != null)
            {
                Currency? currency = await _currenciesRepository.GetByIdAsync(
                    (int)dto.TargetCurrencyId
                );
                if (currency == null)
                {
                    return ServiceResult.Fail(
                        $"Currency with id {dto.TargetCurrencyId} not found",
                        HttpCodes.NotFound
                    );
                }
            }

            jar.Target = (decimal?)dto.Target ?? jar.Target;
            jar.Name = dto.Name ?? jar.Name;
            jar.TargetCurrencyId = dto.TargetCurrencyId ?? jar.TargetCurrencyId;

            await _jarsRepository.Update(jar);

            return ServiceResult.Ok(await GetJarWithBalance(jar));
        }

        public async Task<ServiceResult> DeleteJar(int userId, int jarId)
        {
            Jar? jar = await _jarsRepository.GetByIdAsync(jarId);

            if (jar == null)
            {
                return ServiceResult.Fail($"Jar with id {jarId} not found", HttpCodes.NotFound);
            }

            if (jar.UserId != userId)
            {
                return ServiceResult.Fail(
                    $"You are not the owner of this jar",
                    HttpCodes.Forbidden
                );
            }

            await _jarsRepository.Delete(jar);

            return ServiceResult.Ok(await GetJarWithBalance(jar));
        }

        private async Task<DetailedJarDto> GetJarWithBalance(Jar jar)
        {
            Dictionary<string, decimal> currencyBalances = [];
            foreach (var store in jar.Stores)
            {
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
            }
            return new DetailedJarDto(jar, currencyBalances, _currenciesService);
        }
    }
}
