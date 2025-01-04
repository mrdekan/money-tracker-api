using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Services;

namespace money_tracker.Application.Interfaces
{
    public interface IStoresService
    {
        public Task<ServiceResult> AddStore(CreateStoreDto dto, int userId);
        public Task<ServiceResult> UpdateStore(UpdateStoreDto dto, int userId, int storeId);
        public Task<ServiceResult> DeleteStore(int userId, int storeId);
    }
}
