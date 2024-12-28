using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Repositories;

namespace money_tracker.Application.Services
{
    public class StoresService : IStoresService
    {
        private readonly StoresRepository _storesRepository;
        private readonly JarsRepository _jarsRepository;

        public StoresService(StoresRepository storesRepository, JarsRepository jarsRepository)
        {
            _storesRepository = storesRepository;
            _jarsRepository = jarsRepository;
        }

        public async Task<ServiceResult> AddStore(CreateStoreDto dto, int userId, int jarId)
        {
            Jar jar = await _jarsRepository.GetByIdAsync(jarId);
            if (jar == null)
            {
                return ServiceResult.Fail($"Jar with id {jarId} not found", 404);
            }

            if (jar.UserId != userId)
            {
                return ServiceResult.Fail($"You are not this jar's owner");
            }

            Store store = new()
            {
                Name = dto.Name,
                JarId = jarId,
            };

            await _storesRepository.Add(store);

            return ServiceResult.Ok();
        }
    }
}
