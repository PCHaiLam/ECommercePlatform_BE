using ECommercePlatform.Core.DTOs;
using ECommercePlatform.Core.DTOs.User;

namespace ECommercePlatform.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task LogoutAsync(RefreshTokenRequestDto request);
    }
}


