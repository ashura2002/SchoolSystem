using WebAPI.Controllers;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {

            services.AddScoped<UserController>();
            return services;
        }
    }
}
