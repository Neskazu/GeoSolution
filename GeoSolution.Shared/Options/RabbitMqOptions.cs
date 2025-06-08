using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSolution.Shared.Options
{
    public class RabbitMqOptions
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string VirtualHost { get; set; } = "/";
        public string? QueueName { get; internal set; }
    }
}
