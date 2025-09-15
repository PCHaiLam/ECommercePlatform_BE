using FluentValidation;
using FluentValidation.AspNetCore;

namespace ECommercePlatform.API.Startup.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidationExtensions(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<Core.Validators.User.RegisterRequestValidator>();
            
            return services;
        }
    }
}


