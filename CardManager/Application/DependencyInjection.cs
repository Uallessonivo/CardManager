using CardManager.Application.DTO;
using CardManager.Application.Interfaces;
using CardManager.Application.Services;
using CardManager.Application.Validators;
using FluentValidation;

namespace CardManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CardDto>, CardValidator>();
            services.AddTransient<IValidator<UpdateCardDto>, UpdateCardValidator>();
            services.AddScoped<ICardService, CardService>();
            return services;
        }
    }
}
