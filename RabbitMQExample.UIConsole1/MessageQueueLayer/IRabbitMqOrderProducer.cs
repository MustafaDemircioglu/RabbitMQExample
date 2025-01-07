using RabbitMQExample.UIConsole.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.UIConsole.MessageQueueLayer
{
    public interface IRabbitMqOrderProducer
    {
        void SendOrderToQueue(Order order);
    }
}
