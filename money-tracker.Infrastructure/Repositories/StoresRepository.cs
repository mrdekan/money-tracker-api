using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class StoresRepository : BaseRepository<Store>
    {
        private readonly ApplicationDbContext _context;
        public StoresRepository(ApplicationDbContext context) : base(context, context.Stores)
        {
            _context = context;
        }
    }
}
