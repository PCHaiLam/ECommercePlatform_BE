using ECommercePlatform.Core.DTOs;
using ECommercePlatform.Core.DTOs.User;

namespace ECommercePlatform.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> RegisterAsync(RegisterRequestDto request);
    }
}


