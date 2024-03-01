using RabbitMQ.Client;
using Shared;
using Shared.Models.Dtos;
using System.Text;
using System.Text.Json;

namespace Producer.Services
{
    public interface IProducerService
    {
        bool SendEmail(EmailDto email);
        bool SendNoti(NotificationDto noti);
    }
    public class ProducerService : IProducerService
    {
        private readonly IProducerFactory _producerFactory;
        private readonly ILogger<ProducerService> _logger;
        public ProducerService(IProducerFactory producerFactory, ILogger<ProducerService> logger)
        {
            _producerFactory = producerFactory;
            _logger = logger;
        }
        public bool SendEmail(EmailDto email)
        {
            try
            {
                var properties = _producerFactory.Channel.CreateBasicProperties();
                properties.Persistent = true;
                
                _producerFactory.Channel.BasicPublish(exchange: RabbitMQExchange.ChelseaExchange,
               routingKey: "etihad",
               basicProperties: properties,
               body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(email)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendEmail error");
            }
            
            return true;
        }

        public bool SendNoti(NotificationDto noti)
        {
            try
            {
                IBasicProperties basicProperties = null;
                _producerFactory.Channel.BasicPublish(exchange: RabbitMQExchange.ChelseaExchange,
               routingKey: RoutingKeys.ProductNoti,
               basicProperties: null,
               body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(noti)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendNoti error");
            }

            return true;
        }
    }
}
