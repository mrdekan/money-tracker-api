using money_tracker.Application.Dtos.Response.CurrencyBalances;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;

namespace money_tracker.Application.Dtos.Response.Stores
{
    public class StoreDto
    {
        public StoreDto(Store entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Balance = 0;
            Balances = [];
        }

        public StoreDto(Store entity, ICurrenciesService currenciesService, string targetCurrency)
        {
            Id = entity.Id;
            Name = entity.Name;
            Balances = entity
                .CurrencyBalances.Select(balance => new CurrencyBalanceDto()
                {
                    Currency = currenciesService.GetName(balance.CurrencyId).Result,
                    Balance = balance.Balance,
                })
                .ToList();
            Balance = 0;
            foreach (var currency in Balances)
            {
                if (currency.Currency == targetCurrency)
                {
                    Balance += currency.Balance;
                }
                else
                {
                    Balance += currenciesService
                        .Convert(currency.Balance, currency.Currency, targetCurrency)
                        .Result;
                }
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<CurrencyBalanceDto> Balances { get; set; }
    }
}
