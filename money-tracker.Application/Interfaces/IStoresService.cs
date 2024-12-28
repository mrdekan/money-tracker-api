using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Services;

namespace money_tracker.Application.Interfaces
{
    public interface IStoresService
    {
        public Task<ServiceResult> AddStore(CreateStoreDto dto, int userId, int jarId);
    }
}
