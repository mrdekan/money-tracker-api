using money_tracker.Application.Dtos.Requests.Jar;
using money_tracker.Application.Dtos.Response.Jars;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Repositories;

namespace money_tracker.Application.Services
{
    public class JarsService : IJarsService
    {
        private readonly JarsRepository _jarsRepository;
        private readonly CurrenciesRepository _currenciesRepository;

        public JarsService(JarsRepository jarsRepository, CurrenciesRepository currenciesRepository)
        {
            _jarsRepository = jarsRepository;
            _currenciesRepository = currenciesRepository;
        }

        public async Task<ServiceResult> AddJar(CreateJarDto dto, int userId)
        {
            Currency currency = await _currenciesRepository.GetByIdAsync(dto.TargetCurrencyId);
            if (currency == null)
            {
                return ServiceResult.Fail($"Currency with id {dto.TargetCurrencyId} not found", 404);
            }

            Jar jar = new()
            {
                Name = dto.Name,
                Target = dto.Target,
                TargetCurrencyId = dto.TargetCurrencyId,
                UserId = userId,
            };

            await _jarsRepository.Add(jar);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> UpdateJar(UpdateJarDto dto, int userId, int jarId)
        {
            Jar jar = await _jarsRepository.GetByIdAsync(jarId);

            if (jar == null)
            {
                return ServiceResult.Fail($"Jar with id {jarId} not found", 404);
            }

            jar.Target = dto.Target ?? jar.Target;
            //jar.TargetCurrency = dto.TargetCurrency ?? jar.TargetCurrency;
            jar.Name = dto.Name ?? jar.Name;

            await _jarsRepository.Update(jar);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> GetJars(int userId)
        {
            var jars = await _jarsRepository.GetByUserId(userId);
            return ServiceResult.Ok(jars.Select(jar => new JarDto(jar)).ToList());
        }
    }
}
