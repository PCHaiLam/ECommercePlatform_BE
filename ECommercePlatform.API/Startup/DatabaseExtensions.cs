using ECommercePlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.API.Startup
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ECommerceDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}


