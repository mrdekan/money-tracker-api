using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class CurrenciesRepository : BaseRepository<Currency>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public CurrenciesRepository(ApplicationDbContext context, IMemoryCache cache)
            : base(context, context.Currencies)
        {
            _context = context;
            _cache = cache;
        }

        public override async Task<IEnumerable<Currency>> GetAll()
        {
            return await _context
                .Currencies.Include(currency => currency.TargetedJars)
                .OrderByDescending(currency => currency.TargetedJars.Count())
                .ToListAsync();
        }

        public virtual async Task<Currency?> GetByNameAsync(string name)
        {
            var cacheKey = $"Currency_{name}";

            if (_cache.TryGetValue(cacheKey, out Currency? cachedCurrency))
            {
                return cachedCurrency;
            }

            var currency = await _context.Currencies.Where(c => c.CC == name).FirstOrDefaultAsync();

            if (currency != null)
            {
                _cache.Set(
                    cacheKey,
                    currency,
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
                    }
                );
            }

            return currency;
        }

        public async Task UpdateCurrencies(List<Currency> currencies)
        {
            var existingEntities = _context
                .Currencies.Where(e => currencies.Select(c => c.CC).Contains(e.CC))
                .ToDictionary(e => e.CC);

            foreach (var entity in currencies)
            {
                if (existingEntities.TryGetValue(entity.CC, out var existingEntity))
                {
                    existingEntity.Rate = entity.Rate;
                }
                else
                {
                    _context.Currencies.Add(entity);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
