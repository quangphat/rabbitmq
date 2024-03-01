using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    public interface IWorkerFactory
    {
        IModel Channel { get; }
    }
    public class WorkerFactory : IWorkerFactory
    {
        private readonly IModel _channel;
        private readonly WorkerConfiguration _workerConfiguration;
        public WorkerFactory(IOptions<WorkerConfiguration> workerConfiguration) 
        {
            _workerConfiguration = workerConfiguration.Value;
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Password = "number8",
                UserName ="quangphat"
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            foreach(var item in _workerConfiguration.ConsumerConfigs)
            {
                _channel.QueueDeclare(durable: item.IsDurable ,autoDelete: false, exclusive: false, queue: item.QueueName);
                _channel.QueueBind(queue: item.QueueName,
                        exchange: RabbitMQExchange.ChelseaExchange,
                        routingKey: item.RoutingKey
                        );
            }
            
        }

        public IModel Channel => _channel;
    }
}
