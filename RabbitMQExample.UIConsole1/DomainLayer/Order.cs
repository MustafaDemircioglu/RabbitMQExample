using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExample.UIConsole.DomainLayer
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public decimal Freight { get; set; }
    }
}
