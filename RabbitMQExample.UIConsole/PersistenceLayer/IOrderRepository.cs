using RabbitMQExample.UIConsole.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExample.UIConsole.PersistenceLayer
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
    }
}
