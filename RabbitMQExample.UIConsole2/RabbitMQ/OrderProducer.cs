using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQExample.UIConsole2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.UIConsole2.RabbitMQ
{
    public class OrderProducer
    {
        public void SendOrder(Order order)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "orders_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Sipariş verisini JSON formatında serileştirip mesaj olarak gönderiyoruz
                string message = JsonConvert.SerializeObject(order);  // Siparişi JSON formatına serileştir
                var body = Encoding.UTF8.GetBytes(message);

                // Mesajı kuyruğa gönder
                channel.BasicPublish(exchange: "",
                                     routingKey: "orders_queue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"[x] Sipariş gönderildi: {message}");
            }
        }
    }
}
