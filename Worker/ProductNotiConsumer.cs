using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using Shared.Models.Dtos;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Worker
{
    public class ProductNotiConsumer : BackgroundService
    {
        private readonly ILogger<ProductNotiConsumer> _logger;
        private readonly IWorkerFactory _workerFactory;
        private readonly EventingBasicConsumer _consumer;
        public ProductNotiConsumer(ILogger<ProductNotiConsumer> logger, IWorkerFactory workerFactory)
        {
            _logger = logger;
            _workerFactory = workerFactory;
            _consumer = new EventingBasicConsumer(_workerFactory.Channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while(!stoppingToken.IsCancellationRequested)
            //{
            _consumer.Received += (model, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var email = JsonSerializer.Deserialize<NotificationDto>(message);
                _logger.LogInformation("Receive notification title: {title} content: {content}", email.Title, email.Content);

            };
            _workerFactory.Channel.BasicConsume(queue: QueueNames.ProductNoti, autoAck: true, consumer: _consumer);
            //}
        }
    }
}
