using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommercePlatform.API.Startup
{
    public static class CorsExtensions
    {
        private const string DefaultCors = "DefaultCors";

        public static IServiceCollection AddDefaultCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCors, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            return services;
        }

        public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder app)
        {
            app.UseCors(DefaultCors);
            return app;
        }
    }
}


