using Microsoft.EntityFrameworkCore;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class JarsRepository : BaseRepository<Jar>
    {
        private readonly ApplicationDbContext _context;

        public JarsRepository(ApplicationDbContext context)
            : base(context, context.Jars)
        {
            _context = context;
        }

        public override async Task<Jar?> GetByIdAsync(int id)
        {
            return await _context
                .Jars.Where(jar => jar.Id == id)
                .Include(jar => jar.TargetCurrency)
                .Include(jar => jar.Stores)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Jar>> GetByUserId(int userId)
        {
            return await _context
                .Jars.Where(jar => jar.UserId == userId)
                .Include(jar => jar.TargetCurrency)
                .Include(jar => jar.Stores)
                .ToListAsync();
        }

        public async Task<Jar?> GetByStoreId(int storeId)
        {
            return await _context
                .Jars.Include(jar => jar.Stores)
                .Include(jar => jar.TargetCurrency)
                .Where(jar => jar.Stores.First(s => s.Id == storeId) != null)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameTaken(string name, int userId)
        {
            Jar? jar = await _context
                .Jars.Where(j => j.Name == name && j.UserId == userId)
                .FirstOrDefaultAsync();
            return jar != null;
        }
    }
}
