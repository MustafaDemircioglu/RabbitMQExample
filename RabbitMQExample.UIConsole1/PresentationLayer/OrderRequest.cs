using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.UIConsole.PresentationLayer
{
    public class OrderRequest
    {
        public string CustomerId { get; set; }
        public decimal Freight { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
    }
}
