using Microsoft.EntityFrameworkCore;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class CurrencyBalancesRepository : BaseRepository<CurrencyBalance>
    {
        private readonly ApplicationDbContext _context;
        public CurrencyBalancesRepository(ApplicationDbContext context) : base(context, context.CurrencyBalances)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurrencyBalance>> GetByStoreIdAsync(int storeId)
        {
            return await _context.CurrencyBalances
                .Where(cb => cb.StoreId == storeId)
                .Include(cb => cb.Currency)
                .ToListAsync();
        }

        public async Task<CurrencyBalance> GetByStoreAndCurrencyIdAsync(int storeId, int currencyId)
        {
            CurrencyBalance? balance = await _context.CurrencyBalances.Where(cb => cb.StoreId == storeId && cb.CurrencyId == currencyId).FirstOrDefaultAsync();
            if (balance == null)
            {
                balance = new CurrencyBalance()
                {
                    StoreId = storeId,
                    CurrencyId = currencyId
                };
                await Add(balance);
            }
            return balance;
        }
    }
}
