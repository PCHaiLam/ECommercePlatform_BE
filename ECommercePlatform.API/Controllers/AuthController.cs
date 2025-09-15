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
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}


