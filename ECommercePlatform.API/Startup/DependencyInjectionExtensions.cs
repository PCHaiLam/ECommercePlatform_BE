using Microsoft.Extensions.DependencyInjection;

namespace ECommercePlatform.API.Startup
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}


