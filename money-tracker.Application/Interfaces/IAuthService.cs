using money_tracker.Application.Dtos.Requests.Auth;
using money_tracker.Application.Services;

namespace money_tracker.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult> LoginAsync(SignInDto dto);
        Task<ServiceResult> RegisterAsync(SignUpDto dto);
    }
}
