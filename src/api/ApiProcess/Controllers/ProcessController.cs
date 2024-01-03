using Domain.Entity;
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
        private readonly IConfiguration _configuration;

        public ProcessController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(string name)
        {
            var process = new ProcessEntity(name);

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
