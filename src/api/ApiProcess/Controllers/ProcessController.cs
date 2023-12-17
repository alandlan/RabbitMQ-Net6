﻿using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ApiProcess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(string name)
        {
            var process = new ProcessEntity(name);

            var factory = new ConnectionFactory()
            {
                //HostName = "rabbitMq",
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

                var message = JsonSerializer.Serialize(process);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "processQueue",
                    basicProperties: null,
                    body: body);
            };

            return Ok(process);
        }
    }
}
