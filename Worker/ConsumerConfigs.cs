using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    public static class ConsumerConfigs
    {
        public static List<ConsumerConfig> Consumers = new List<ConsumerConfig>()
        {
            new ConsumerConfig
            {
                QueueName = QueueNames.ProductEmail,
                RoutingKey = RoutingKeys.ProductEmail,
            },
            new ConsumerConfig
            {
                QueueName = QueueNames.ProductNoti,
                RoutingKey = RoutingKeys.ProductNoti,
            },
        };
    }
}
