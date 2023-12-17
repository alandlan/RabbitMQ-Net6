using Domain.Repositories;
using Infrastructure.Database.Core;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfraConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });

            services.AddScoped<SqlServerContext>();
            services.AddScoped<IProcessRepository, ProcessRepository>();

            using var serviceProvider = services.BuildServiceProvider();
            using var context = serviceProvider.GetRequiredService<SqlServerContext>();

            context.Database.Migrate();

            return services;
        }
    }
}
