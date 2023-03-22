using CardManager.Infrastructure.Interfaces;
using CardManager.Infrastructure.Persistence;
using CardManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CardManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CardManagerDbContextInMemory>(options =>
                options.UseInMemoryDatabase("CardManagerDb"));
            services.AddScoped<ICardRepository, CardRepository>();
            return services;
        }
    }
}
