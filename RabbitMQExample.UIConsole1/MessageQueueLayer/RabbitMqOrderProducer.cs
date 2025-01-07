using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQExample.UIConsole.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.UIConsole.MessageQueueLayer
{
    public class RabbitMqOrderProducer : IRabbitMqOrderProducer
    {
        private readonly string _hostname = "localhost"; // RabbitMQ sunucusu
        private readonly string _queueName = "order_queue";
        void IRabbitMqOrderProducer.SendOrderToQueue(Order order)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var orderMessage = new { Order = order};
            var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(orderMessage));

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: messageBody);

            Console.WriteLine(" [x] Sent {0}", JsonConvert.SerializeObject(orderMessage));
        }


    }
}
