using money_tracker.Application.Dtos.Requests.Jar;
using money_tracker.Application.Services;

namespace money_tracker.Application.Interfaces
{
    public interface IJarsService
    {
        public Task<ServiceResult> AddJar(CreateJarDto dto, int userId);
        public Task<ServiceResult> UpdateJar(UpdateJarDto dto, int userId, int jarId);
        public Task<ServiceResult> GetJars(int userId);
    }
}
