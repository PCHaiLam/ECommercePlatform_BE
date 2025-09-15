using ECommercePlatform.Application.Interfaces;
using ECommercePlatform.Application.Services;
using ECommercePlatform.Core.Interfaces;
using ECommercePlatform.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ECommercePlatform.API.Startup.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            // Services
            services.AddScoped<IAuthService, AuthService>();


            return services;
        }
    }
}


