using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Dtos.Response.Stores;
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

        public async Task<ServiceResult> AddStore(CreateStoreDto dto, int userId)
        {
            Jar jar = await _jarsRepository.GetByIdAsync(dto.JarId);
            if (jar == null)
            {
                return ServiceResult.Fail($"Jar with id {dto.JarId} not found", HttpCodes.NotFound);
            }

            if (jar.UserId != userId)
            {
                return ServiceResult.Fail($"You are not the owner of this jar", HttpCodes.Forbidden);
            }

            Store store = new()
            {
                Name = dto.Name,
                JarId = dto.JarId,
            };

            await _storesRepository.Add(store);

            return ServiceResult.Ok(new StoreDto(store));
        }
    }
}
