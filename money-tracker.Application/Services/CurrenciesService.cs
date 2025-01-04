using money_tracker.Application.Dtos.Response.Currencies;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Repositories;

namespace money_tracker.Application.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly CurrenciesRepository _currenciesRepository;
        private readonly string _baseCurrency;

        public CurrenciesService(
            CurrenciesRepository currenciesRepository,
            ConfigManager configuration
        )
        {
            _currenciesRepository = currenciesRepository;
            _baseCurrency = configuration.BaseCurrency;
        }

        public async Task<string> GetName(int id)
        {
            return (await _currenciesRepository.GetByIdAsync(id)).CC;
        }

        public async Task<decimal> Convert(decimal amount, string from, string to)
        {
            if (to == _baseCurrency)
            {
                return await ToBase(amount, from);
            }
            if (from == _baseCurrency)
            {
                return await FromBase(amount, to);
            }
            return await FromBase(await ToBase(amount, from), to);
        }

        public async Task<decimal> Convert(decimal amount, int from, int to)
        {
            Currency? toCurrency = await _currenciesRepository.GetByIdAsync(from);
            Currency? fromCurrency = await _currenciesRepository.GetByIdAsync(from);
            if (toCurrency == null || fromCurrency == null)
            {
                return 0;
            }
            if (toCurrency.CC == _baseCurrency)
            {
                return ToBase(amount, fromCurrency);
            }
            if (fromCurrency.CC == _baseCurrency)
            {
                return FromBase(amount, toCurrency);
            }
            return FromBase(ToBase(amount, fromCurrency), toCurrency);
        }

        private async Task<decimal> ToBase(decimal amout, string from)
        {
            Currency? currency = await _currenciesRepository.GetByNameAsync(from);
            return currency == null ? 0 : ToBase(amout, currency);
        }

        private async Task<decimal> FromBase(decimal amout, string to)
        {
            Currency? currency = await _currenciesRepository.GetByNameAsync(to);
            return currency == null ? 0 : FromBase(amout, currency);
        }

        private decimal ToBase(decimal amout, Currency from)
        {
            return amout * (decimal)from.Rate;
        }

        private decimal FromBase(decimal amout, Currency to)
        {
            return amout / (decimal)to.Rate;
        }

        public async Task<ServiceResult> GetAll()
        {
            var currencies = await _currenciesRepository.GetAll();
            return ServiceResult.Ok(currencies.Select(currency => new CurrencyDto(currency)));
        }
    }
}
