// See https://aka.ms/new-console-template for more information

using RabbitMQExample.UIConsole2.Model;
using RabbitMQExample.UIConsole2.RabbitMQ;

var producer = new OrderProducer();
var order = new Order
{
    CustomerID = "ALFKI",
    OrderDate = DateTime.Now,
    ShippedDate = DateTime.Now.AddDays(2),
    ShipAddress = "Test Adresi",
    ShipCity = "Test Şehri",
    OrderDetails = new List<OrderDetail>  // Sipariş detayları
    {
        new OrderDetail { ProductID = 1, Quantity = 10, UnitPrice = 25.50m },
        new OrderDetail { ProductID = 2, Quantity = 5, UnitPrice = 15.75m }
    }
};
producer.SendOrder(order);


Console.WriteLine("Siparişleri İşleyelim mi? (E/H)");
var orderConfirmation = Console.ReadLine();

if (orderConfirmation =="E")
{
    var consumer = new OrderConsumer();
    consumer.StartConsuming();

}
