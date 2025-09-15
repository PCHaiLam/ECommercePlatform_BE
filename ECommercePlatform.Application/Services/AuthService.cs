using ECommercePlatform.Application.Interfaces;
using ECommercePlatform.Core.Common;
using ECommercePlatform.Core.DTOs;
using ECommercePlatform.Core.Entities;
using ECommercePlatform.Core.DTOs.User;
using ECommercePlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommercePlatform.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ECommerceDbContext _db;
        private readonly IConfiguration _config;

        const string ACTIVE_STATUS = "active";
        const string DEFAULT_IMAGE_URL = "https://res.cloudinary.com/dxg04terf/image/upload/v1757927319/samples/animals/cat.jpg";

        public AuthService(ECommerceDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<ApiResponse> RegisterAsync(RegisterRequestDto request)
        {
            if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            {
                return ApiResponse.ErrorResult(new List<string> { "Email already exists" });
            }

            var user = new User
            {
                Email = request.Email,
                PasswordHash = PasswordHelper.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                AvatarUrl = DEFAULT_IMAGE_URL,
                EmailVerified = false,
                Status = ACTIVE_STATUS,
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // Generate tokens
            var jwtSection = _config.GetSection("Jwt");
            var accessToken = JwtHelper.GenerateToken(
                user.Id,
                user.Email,
                roles: null,
                secretKey: jwtSection["AccessTokenSecret"],
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                expiryMinutes: int.TryParse(jwtSection["AccessTokenMinutes"], out var atExp) ? atExp : 60
            );

            var refreshTokenString = JwtHelper.GenerateRefreshToken();
            var refreshExpiresMinutes = int.TryParse(jwtSection["RefreshTokenMinutes"], out var rtExp) ? rtExp : 1440;
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshTokenString,
                ExpiresAt = DateTime.UtcNow.AddMinutes(refreshExpiresMinutes),
            };

            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();

            var responseData = new
            {
                accessToken,
                refreshToken = refreshTokenString,
            };

            return new ApiResponse
            {
                Success = true,
                Message = "Registered",
                Data = responseData
            };
        }
    }
}


