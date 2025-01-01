using money_tracker.Application.Services;

namespace money_tracker.Application.Interfaces
{
    public interface ICurrenciesService
    {
        public Task<decimal> Convert(decimal amount, string from, string to);
        public Task<ServiceResult> GetAll();
    }
}
