using money_tracker.Domain.Entities;

namespace money_tracker.Application.Dtos.Response.Currencies
{
    public class CurrencyDto
    {
        public CurrencyDto(Currency entity)
        {
            Name = entity.CC;
            Rate = entity.Rate;
        }

        public string Name { get; set; }
        public double Rate { get; set; }
    }
}
