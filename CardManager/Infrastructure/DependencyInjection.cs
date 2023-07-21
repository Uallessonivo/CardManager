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
            services.AddDbContext<CardManagerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DevelopConnection")));
            services.AddScoped<ICardRepository, CardRepository>();
            return services;
        }
    }
}
