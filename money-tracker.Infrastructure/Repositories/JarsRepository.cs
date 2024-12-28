using Microsoft.EntityFrameworkCore;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class JarsRepository : BaseRepository<Jar>
    {
        private readonly ApplicationDbContext _context;
        public JarsRepository(ApplicationDbContext context) : base(context, context.Jars)
        {
            _context = context;
        }

        public async Task<IEnumerable<Jar>> GetByUserId(int userId)
        {
            return await _context.Jars.Where(jar => jar.UserId == userId).ToListAsync();
        }
    }
}
