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
                                // .AddJsonFile($"appsettings.{configuration["Environment"]}.json", true)
                                
                                .AddEnvironmentVariables()
                                .Build());

        configuration = c.Build();

        //print connection string
        // Console.WriteLine($"Connection String: {configuration.GetConnectionString("SqlConnection")}");

        // //print environment
        // Console.WriteLine($"Environment: {configuration["Environment"]}");

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
