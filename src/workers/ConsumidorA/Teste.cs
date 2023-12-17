using Domain.Entity;
using Domain.UseCases;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;

namespace ConsumidorA
{
    public sealed class Teste : BackgroundService
    {
        private readonly IProcessUseCase _processUseCase;
        private readonly IServiceProvider _serviceProvider;

        public Teste(IServiceProvider serviceProvider)
        {
            //_processUseCase = processUseCase;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var _processUseCase = scope.ServiceProvider.GetRequiredService<IProcessUseCase>();


            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);

                var factory = new ConnectionFactory()
                {
                    HostName = "rabbitmq",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                };

                //var factory = new ConnectionFactory
                //{
                //    Uri = new Uri("amqp://guest:guest@rabbitmq:5672")
                //};

                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "processQueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

   

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var process = JsonSerializer.Deserialize<ProcessEntity>(message);

                        if (process != null)
                            process.ProcessAt = DateTime.Now;

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
