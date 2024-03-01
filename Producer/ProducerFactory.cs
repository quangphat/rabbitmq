using RabbitMQ.Client;
using Shared;

namespace Producer
{
    public interface IProducerFactory
    {
        IModel Channel { get; }
    }
    public class ProducerFactory : IProducerFactory
    {
        private readonly IModel _channel;
        public ProducerFactory()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Password = "number8",
                UserName = "quangphat"
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.ExchangeDeclare(exchange: RabbitMQExchange.ChelseaExchange, type: ExchangeType.Direct);
        }

        public IModel Channel => _channel;
    }
}
