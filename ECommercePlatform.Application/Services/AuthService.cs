using AutoMapper;
using ECommercePlatform.Application.Interfaces;
using ECommercePlatform.Core.Common;
using ECommercePlatform.Core.Entities;
using ECommercePlatform.Core.DTOs.User;
using ECommercePlatform.Core.Exceptions;
using ECommercePlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommercePlatform.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ECommerceDbContext _db;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(ECommerceDbContext db, IConfiguration config, IMapper mapper)
        {
            _db = db;
            _config = config;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            {
                throw new ValidationException("Email already exists");
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = PasswordHelper.HashPassword(request.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // Generate tokens
            var jwtSection = _config.GetSection("JwtConfig");
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

            return CreateAuthResponse(user, accessToken, refreshTokenString, jwtSection);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new ValidationException("Invalid email or password");
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
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

            return CreateAuthResponse(user, accessToken, refreshTokenString, jwtSection);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var refreshToken = await _db.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.ExpiresAt > DateTime.UtcNow);
            
            if (refreshToken == null)
            {
                throw new ValidationException("Invalid or expired refresh token");
            }

            var user = refreshToken.User;
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            // Generate new tokens
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

            var newRefreshTokenString = JwtHelper.GenerateRefreshToken();
            
            // Remove old refresh token
            _db.RefreshTokens.Remove(refreshToken);
            
            // Add new refresh token
            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshTokenString,
                ExpiresAt = DateTime.UtcNow.AddMinutes(int.TryParse(jwtSection["RefreshTokenMinutes"], out var rtExp) ? rtExp : 1440),
            };
            
            _db.RefreshTokens.Add(newRefreshToken);
            await _db.SaveChangesAsync();

            return CreateAuthResponse(user, accessToken, newRefreshTokenString, jwtSection);
        }

        public async Task LogoutAsync(RefreshTokenRequestDto request)
        {
            var refreshToken = await _db.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);
            
            if (refreshToken != null)
            {
                _db.RefreshTokens.Remove(refreshToken);
                await _db.SaveChangesAsync();
            }
        }

        private AuthResponseDto CreateAuthResponse(User user, string accessToken, string refreshToken, Microsoft.Extensions.Configuration.IConfigurationSection jwtSection)
        {
            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(int.TryParse(jwtSection["AccessTokenMinutes"], out var exp) ? exp : 60),
                User = _mapper.Map<UserDto>(user)
            };
        }
    }
}


