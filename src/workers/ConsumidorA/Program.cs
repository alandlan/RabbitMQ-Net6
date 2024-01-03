using ConsumidorA;
using DependencyInjection;

IConfiguration configuration = default;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(c =>
    {
        configuration = c.Build();

        c.AddConfiguration(new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{configuration["Environment"]}.json", true)
                                
                                .AddEnvironmentVariables()
                                .Build());

        configuration = c.Build();
    })
    .ConfigureServices((builder, services) =>
    {
        services.AddWorkerServices(configuration);

        services.AddHostedService<Worker>();

    });

var host = builder.Build();
host.Run();
