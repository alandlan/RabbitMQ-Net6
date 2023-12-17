using Microsoft.Extensions.DependencyInjection;
using App;
using Microsoft.Extensions.Configuration;
using Infrastructure;

namespace DependencyInjection
{
    public static class Injection
    {
        public static IServiceCollection AddWorkerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppServices()
                    .AddDatabase(configuration);

            return services;
        }
    }
}
