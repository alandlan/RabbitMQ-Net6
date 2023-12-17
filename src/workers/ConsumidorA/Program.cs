using App.UseCases;
using ConsumidorA;
using Domain.UseCases;
using Infrastructure;
using DependencyInjection;

IConfiguration configuration = default;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(c =>
    {
        c.AddConfiguration(new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .Build());

        configuration = c.Build();

    })
    .ConfigureServices((builder, services) =>
    {
        services.AddWorkerServices(configuration);
        //services.AddScoped<IProcessUseCase, ProcessUseCase>();
        //services.AddDatabase(configuration);


        //services.AddScoped<IProcessRepository, ProcessRepository>();

        services.AddHostedService<Worker>();

    });

var host = builder.Build();
host.Run();
