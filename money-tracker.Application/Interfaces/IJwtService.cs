using money_tracker.Domain.Entities;

namespace money_tracker.Application.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user);
    }
}
