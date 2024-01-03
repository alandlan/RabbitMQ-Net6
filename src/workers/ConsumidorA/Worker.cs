using Domain.Entity;
using Domain.UseCases;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ConsumidorA
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);

                var factory = new ConnectionFactory
                {
                    Uri = new Uri(uriString: _configuration.GetConnectionString("RabbitMqConnection"))
                };

                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    

                    channel.QueueDeclare(queue: "processQueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    ProcessEntity process = null;

                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        process = JsonSerializer.Deserialize<ProcessEntity>(message);

                        if(process != null)
                            process.ProcessAt = DateTime.Now;

                        _logger.LogInformation($"Processo recebido: {process?.ToString()}");

                        if (process != null && !String.IsNullOrEmpty(process.Name))
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var _processUseCase = scope.ServiceProvider.GetRequiredService<IProcessUseCase>();

                            await _processUseCase.CreateAsync(process);
                        }
                    };

                    channel.BasicConsume(queue: "processQueue",
                                        autoAck: true,
                                        consumer: consumer);

                    await Task.Delay(5000, stoppingToken);
                }
            }
        }
    }
}
