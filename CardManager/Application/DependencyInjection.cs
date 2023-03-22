using CardManager.Application.Interfaces;
using CardManager.Application.Services;

namespace CardManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICardService, CardService>();
            return services;
        }
    }
}
