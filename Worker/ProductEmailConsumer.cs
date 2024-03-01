using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using Shared.Models.Dtos;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Worker
{
    public class ProductEmailConsumer : BackgroundService
    {
        private readonly ILogger<ProductEmailConsumer> _logger;
        private readonly IWorkerFactory _workerFactory;
        private readonly EventingBasicConsumer _consumer;
        public ProductEmailConsumer(ILogger<ProductEmailConsumer> logger, IWorkerFactory workerFactory)
        {
            _logger = logger;
            _workerFactory = workerFactory;
            _consumer = new EventingBasicConsumer(_workerFactory.Channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while(!stoppingToken.IsCancellationRequested)
            //{
            
            _consumer.Received += async (model, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var email = JsonSerializer.Deserialize<EmailDto>(message);
                //await Task.Delay(10000);
                _logger.LogInformation("Receive email content: {content} - subject: {subject} - id: {id}", email.Email, email.Subject, email.Id);

            };
            _workerFactory.Channel.BasicConsume(queue: "manchestercity", autoAck: false, consumer: _consumer);
            //}
        }
    }
}
