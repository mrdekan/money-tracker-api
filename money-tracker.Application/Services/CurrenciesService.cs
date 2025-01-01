using Microsoft.Extensions.Configuration;
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
            IConfiguration configuration
        )
        {
            _currenciesRepository = currenciesRepository;
            _baseCurrency = configuration["Currency:Default"];
            if (string.IsNullOrEmpty(_baseCurrency))
            {
                throw new Exception("Default currency not found in appsettings.json");
            }
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

        private async Task<decimal> ToBase(decimal amout, string from)
        {
            Currency? currency = await _currenciesRepository.GetByNameAsync(from);
            return currency == null ? 0 : amout * (decimal)currency.Rate;
        }

        private async Task<decimal> FromBase(decimal amout, string to)
        {
            Currency? currency = await _currenciesRepository.GetByNameAsync(to);
            return currency == null ? 0 : amout / (decimal)currency.Rate;
        }

        public async Task<ServiceResult> GetAll()
        {
            var currencies = await _currenciesRepository.GetAll();
            return ServiceResult.Ok(currencies.Select(currency => new CurrencyDto(currency)));
        }
    }
}
