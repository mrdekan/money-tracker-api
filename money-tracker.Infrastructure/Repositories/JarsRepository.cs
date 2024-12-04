using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class JarsRepository : BaseRepository<Jar>
    {
        public JarsRepository(ApplicationDbContext context) : base(context, context.Jars) { }
    }
}
