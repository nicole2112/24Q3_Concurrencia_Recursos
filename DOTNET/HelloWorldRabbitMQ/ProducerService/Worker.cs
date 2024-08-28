using RabbitMQ.Client;
using System.Text;

namespace ProducerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672,
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "hello-queue",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                        var message = "Hello world";
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: string.Empty,
                                            routingKey: "hello-queue",
                                            basicProperties: null,
                                            body: body);

                        Console.WriteLine($"Mensaje enviado: {message}");
                    }
                }

                return;
            }
        }
    }
}
