using App.UseCases;
using Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public static class AppModule
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProcessUseCase, ProcessUseCase>();

            return services;
        }
    }
}
