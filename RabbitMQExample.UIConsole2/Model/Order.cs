using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.UIConsole2.Model
{
    public class Order
    {
        public int OrderID { get; set; }  // Identity alanı
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        
    }
}
