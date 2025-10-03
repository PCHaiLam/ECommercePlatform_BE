using ECommercePlatform.Application.Interfaces;
using ECommercePlatform.Core.DTOs;
using ECommercePlatform.Core.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePlatform.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(ApiResponse<AuthResponseDto>.Ok(result));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(ApiResponse<AuthResponseDto>.Ok(result));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            return Ok(ApiResponse<AuthResponseDto>.Ok(result));
        }

        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponse<object>>> Logout([FromBody] RefreshTokenRequestDto request)
        {
            await _authService.LogoutAsync(request);
            return Ok(ApiResponse<object>.Ok(new { message = "Logged out successfully" }));
        }
    }
}


