using RabbitMQExample.UIConsole.DomainLayer;
using RabbitMQExample.UIConsole.MessageQueueLayer;
using RabbitMQExample.UIConsole.PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.UIConsole.ApplicationLayer
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRabbitMqOrderProducer _orderProducer;

        public OrderService(IOrderRepository orderRepository, IRabbitMqOrderProducer orderProducer)
        {
            _orderRepository = orderRepository;
            _orderProducer = orderProducer;
        }

        public void CreateOrder(Order order)
        {
            // Veritabanına siparişi ekle/
            _orderRepository.AddOrder(order);

            // RabbitMQ'ya sipariş mesajı gönder
            _orderProducer.SendOrderToQueue(order);
        }

    }
}
