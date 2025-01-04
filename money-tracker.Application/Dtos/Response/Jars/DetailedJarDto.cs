using money_tracker.Application.Dtos.Response.CurrencyBalances;
using money_tracker.Application.Dtos.Response.Stores;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;

namespace money_tracker.Application.Dtos.Response.Jars
{
    public class DetailedJarDto : JarDto
    {
        public DetailedJarDto(
            Jar jar,
            Dictionary<string, decimal> currencies,
            ICurrenciesService currencyService
        )
            : base(jar, currencies, currencyService)
        {
            Stores = jar
                .Stores.Select(store => new StoreDto(store, currencyService, jar.TargetCurrency.CC))
                .ToList();
            Currencies = [];
            foreach (var currency in currencies.Keys)
            {
                Currencies.Add(
                    new CurrencyBalanceDto() { Currency = currency, Balance = currencies[currency] }
                );
            }
        }

        public List<StoreDto> Stores { get; set; }
        public List<CurrencyBalanceDto> Currencies { get; set; }
    }
}
