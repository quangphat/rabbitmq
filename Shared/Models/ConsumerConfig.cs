using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class WorkerConfiguration
    {
        public List<ConsumerConfig> ConsumerConfigs { get; set; }
    }
    public class ConsumerConfig
    {
        public string QueueName { get; set; } = null!;
        public string RoutingKey { get; set; } = null!;
        public bool IsDurable { get; set; }
    }
}
