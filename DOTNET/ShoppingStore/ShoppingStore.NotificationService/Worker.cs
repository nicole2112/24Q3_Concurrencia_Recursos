using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace ShoppingStore.NotificationService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orders-queue",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Received += (model, content) =>
            {
                var body = content.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var orderInformation = JsonConvert.DeserializeObject<OrderInformation>(json);

                var message = "mensaje: {1}, enviado a: {2}, id orden: {3}";
                _logger.LogInformation(message, orderInformation.Message, orderInformation.Address, orderInformation.OrderId);
            };

            _channel.BasicConsume(queue: "orders-queue",
                                  autoAck: false,
                                  consumer: _consumer);

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
