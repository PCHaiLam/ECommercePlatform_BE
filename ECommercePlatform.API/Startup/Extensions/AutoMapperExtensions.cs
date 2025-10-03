using ECommercePlatform.Application.Mapping;

namespace ECommercePlatform.API.Startup.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMappingProfile));
            
            return services;
        }
    }
}
