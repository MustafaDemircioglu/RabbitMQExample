using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQExample.UIConsole.ApplicationLayer;
using RabbitMQExample.UIConsole.DomainLayer;
using RabbitMQExample.UIConsole.MessageQueueLayer;
using RabbitMQExample.UIConsole.PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitMQExample.UIConsole.PresentationLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Kullanıcıdan sipariş bilgilerini al
       

            // OrderService ve diğer bağımlılıkları oluştur
            var orderRepository = new SqlOrderRepository();
            var rabbitMqOrderProducer = new RabbitMqOrderProducer();
            var orderService = new OrderService(orderRepository, rabbitMqOrderProducer);

            // Siparişi oluştur
            var order = new Order
            {
                CustomerId = "VINET",
                OrderDate = DateTime.UtcNow,
                RequiredDate = DateTime.UtcNow.AddDays(7),
                Freight = Convert.ToDecimal(5000)
            };

            // Siparişi oluştur ve RabbitMQ'ya gönder
            orderService.CreateOrder(order);

            // Başarılı mesaj
            Console.WriteLine("Sipariş başarıyla oluşturuldu ve üretim sürecine iletildi!");
        }

   }
}
